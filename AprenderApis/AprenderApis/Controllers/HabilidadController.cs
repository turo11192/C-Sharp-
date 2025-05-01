using AprenderApis.Helpers;
using AprenderApis.Models;
using AprenderApis.Services;
using Microsoft.AspNetCore.Mvc;

namespace AprenderApis.Controllers
{
    [ApiController]
    [Route("api/mandril/{idMandril}/[controller]")]
    public class HabilidadController : ControllerBase
    {        
        [HttpGet]
        public ActionResult<IEnumerable<Habilidad>> GetHabilidades(int idMandril)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);

            return Ok(mandril.Habilidades);
        }

        [HttpGet("{idHabilidad}")]
        public ActionResult<Habilidad> GetHabilidad(int idMandril, int idHabilidad)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);

            var habilidad = mandril.Habilidades?.FirstOrDefault(h =>  h.Id == idHabilidad);

            if (habilidad == null)
                return NotFound(Mensajes.Habilidad.NotFound);

            return Ok(habilidad);
        }

        [HttpPost]
        public ActionResult<Habilidad> PostHabilidades(int idMandril, HabilidadInsert habilidadInsert)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);

            var habilidadExistente = mandril.Habilidades.FirstOrDefault(h => h.nombre == habilidadInsert.nombre);

            if (habilidadExistente != null)
                return BadRequest(Mensajes.Habilidad.NotFound);


            var maxHabilidad = mandril.Habilidades.Any() ? mandril.Habilidades.Max(h => h.Id) : 0;

            var habilidadNueva = new Habilidad()
            {
                Id = maxHabilidad + 1,
                nombre = habilidadInsert.nombre,
                potencia = habilidadInsert.potencia
            };

            mandril.Habilidades.Add(habilidadNueva);

            return CreatedAtAction(nameof(GetHabilidad),
                new { idMandril = idMandril, idHabilidad = habilidadNueva.Id },
                habilidadNueva
            );
        }

        [HttpPut("{idHabilidad}")]
        public ActionResult<Habilidad> PutHabilidades(int idMandril, int idHabilidad, HabilidadInsert habilidadInsert)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);

            var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.nombre == habilidadInsert.nombre);

            if (habilidadExistente == null)
                return BadRequest(Mensajes.Habilidad.NotFound);


            var habilidadMismoNombre = mandril.Habilidades?
                .FirstOrDefault(h => h.Id != idHabilidad && h.nombre != habilidadInsert.nombre);

            if (habilidadMismoNombre != null)
                return BadRequest(Mensajes.Habilidad.NombreExistente);

            habilidadExistente.nombre = habilidadInsert.nombre;
            habilidadExistente.potencia = habilidadInsert.potencia;

            return NoContent();
        }

        [HttpDelete]
        public ActionResult<Habilidad> DeleteHabilidades(int idMandril, int idHabilidad)
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == idMandril);

            if (mandril == null)
                return NotFound(Mensajes.Mandril.NotFound);

            var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Id == idHabilidad);

            if (habilidadExistente == null)
                return BadRequest(Mensajes.Habilidad.NotFound);

            mandril.Habilidades?.Remove(habilidadExistente);

            return NoContent();
        }
    }
}
