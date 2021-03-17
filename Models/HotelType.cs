using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class HotelType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Plantilla")]
        public string Template { get; set; }
        // Cosas relacionadas a un tipo de hotel como una plantilla para una promoción
        // no deberían ser atributos en el modelo de tipo de hotel para no sobrecargarlo
        // pero en este caso, prima simplificar la arquitectura
    }
}
