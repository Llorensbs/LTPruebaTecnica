
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    [Display(Name = "Reserva")]
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [Display(Name = "Elementos")]
        public virtual ICollection<Hotel> Hotels { get; set; }
        [Display(Name = "Nº Pasajeros")]
        public int PaxesCount { get; set; }
        [Display(Name = "Fecha Entrada")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Fecha Salida")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Beneficio")]
        public double Profit { get; set; }
        [Display(Name = "Divisa")]
        public string Currency { get; set; }


    }
}
