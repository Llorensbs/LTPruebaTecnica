using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    [Display(Name = "País")]
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
    }
}
