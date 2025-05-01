using AprenderApis.Models;

namespace AprenderApis.Services
{
    public class MandrilDataStore
    {
        public List<Mandril> Mandriles {  get; set; }

        public static MandrilDataStore Current { get; } = new MandrilDataStore();

        public MandrilDataStore()
        {
            Mandriles = new List<Mandril>()
            {
                new Mandril()
                {
                    Id = 1,
                    Nombre = "Juan",
                    Habilidades = new List<Habilidad>
                    {
                        new Habilidad()
                        {
                            Id = 1,
                            nombre = "Saltar",
                            potencia = Habilidad.EPotencia.Moderado
                        }
                    }
                },
                new Mandril()
                {
                    Id = 2,
                    Nombre = "SuperMandril",
                    Habilidades = new List<Habilidad>
                    {
                        new Habilidad()
                        {
                            Id = 1,
                            nombre = "Saltar",
                            potencia = Habilidad.EPotencia.Intenso
                        },
                        new Habilidad()
                        {
                            Id = 2,
                            nombre = "Nadar",
                            potencia = Habilidad.EPotencia.Moderado
                        },
                        new Habilidad()
                        {
                            Id = 3,
                            nombre = "Mamar",
                            potencia = Habilidad.EPotencia.Extremo
                        }
                    }
                },
                new Mandril()
                {
                    Id = 3,
                    Nombre = "Ruka",
                    Habilidades = new List<Habilidad>
                    {
                        new Habilidad()
                        {
                            Id = 1,
                            nombre = "Nadar",
                            potencia = Habilidad.EPotencia.Intenso
                        },
                        new Habilidad()
                        {
                            Id = 2,
                            nombre = "Correr",
                            potencia = Habilidad.EPotencia.Extremo
                        },
                        new Habilidad()
                        {
                            Id = 3,
                            nombre = "Vomitar",
                            potencia = Habilidad.EPotencia.Extremo
                        }
                    }
                }
            };
        }
    }
}
