using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    [Display(Name = "Hotel")]
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        public int HotelTypeId { get; set; }
        [Display(Name = "Tipo")]
        public virtual HotelType HotelType { get; set; }
        [Display(Name = "País")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        // Prestaciones del hotel (para preferencias de cliente)
        [Display(Name = "Puntuación")]
        public int Rating { get; set; }
        [Display(Name = "Permite Grupos")]
        public bool AllowGroups { get; set; }
        [Display(Name = "Tiene Actividades")]
        public bool Activities { get; set; }
        [Display(Name = "Solo Adultos")]
        public bool OnlyAdults { get; set; }
        [Display(Name = "Reservas")]
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
