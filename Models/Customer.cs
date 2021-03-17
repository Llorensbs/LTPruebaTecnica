using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    [Display(Name = "Cliente")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        public string Email { get; set; }
        [Display(Name = "Recibir SMS")]
        public bool RecieveSms { get; set; }
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Telphone { get; set; }
        [Display(Name = "Nivel de Promoción")]
        public Enums.Enums.PromotionLevel PromotionLevel { get; set; }
    }

    

}
