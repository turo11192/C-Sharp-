using AprenderApis.Helpers;
using AprenderApis.Models;
using AprenderApis.Services;
using Microsoft.AspNetCore.Mvc;

namespace AprenderApis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MandrilController : ControllerBase
    {
        //Mostrar todos los mandriles
        [HttpGet]
        public ActionResult<IEnumerable<Mandril>> GetMandriles()
        {
            return Ok(MandrilDataStore.Current.Mandriles);
        }

        //Mostrar mandril por id
        [HttpGet("{idMandril}")]
        public ActionResult<Mandril> GetMandriles(int idMandril)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);
            
            return Ok(mandril);
        }

        //Agregar mandril
        [HttpPost]
        public ActionResult<Mandril> PostMandril(MandrilInsert mandrilInsert)
        {
            var maxMandrilId = MandrilDataStore.Current.Mandriles.Max(x => x.Id);

            var mandrilNuevo = new Mandril()
            {
                Id = maxMandrilId + 1,
                Nombre = mandrilInsert.Nombre,
            };

            MandrilDataStore.Current.Mandriles.Add(mandrilNuevo);

            return CreatedAtAction(nameof(GetMandriles), 
                new { idMandril = mandrilNuevo.Id },
                mandrilNuevo
            );
        }

        //Editar Mandril
        [HttpPut("{idMandril}")]
        public ActionResult<Mandril> PutMandril(int idMandril, MandrilInsert mandrilInsert)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);

            mandril.Nombre = mandrilInsert.Nombre;

            return NoContent();
        }

        //Eliminar Mandril
        [HttpDelete("{idMandril}")]
        public ActionResult<Mandril> DeleteMandril (int idMandril)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);

            MandrilDataStore.Current.Mandriles.Remove(mandril);

            return NoContent();
        }
    }
}
