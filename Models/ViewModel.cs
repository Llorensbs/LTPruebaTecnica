using PruebaTecnica.Models;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class ViewModel
    {
        [Key]
        public int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Country Country { get; set; }
        public virtual HotelType HotelType { get; set; }
    }
}
