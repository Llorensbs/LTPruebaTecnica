using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class Preferencias
    {
        // Instanciamos esta clase en base al histórico de reservas del cliente
        public int TotalBookings { get; set; }
        public int AllowGroupCount { get; set; }
        public int OnlyAdultsCount { get; set; }
        public int ActivitiesCount { get; set; }
        public double RatingMean { get; set; }
        public bool OnlyAdults { get; set; }
        public bool AllowGroup { get; set; }
        public bool Activities { get; set; }

        public void SetPreferencias(Enums.Enums.PromotionLevel nivel)
        {
            if (nivel == Enums.Enums.PromotionLevel.Zero) throw new Exception("No deberían necesitarse estas preferencias para un cliente que no desea recibir promociones");
            // Esta función es para llamar a posteriori si necesitamos tener los bool seteados
            int umbral = 0;
            switch (nivel)
            {
                case Enums.Enums.PromotionLevel.Low:
                    umbral = 75; break;
                case Enums.Enums.PromotionLevel.Mid:
                    umbral = 20; break;
                case Enums.Enums.PromotionLevel.High:
                    umbral = 0; break; // No discriminamos
            }
            // Lo interesante es que si alguien quiere recibir poca promoción
            // la reciba de hoteles de las características que siempre reserva, si las hay
            OnlyAdults = 100 * OnlyAdultsCount / TotalBookings > umbral || umbral == 0;
            AllowGroup = 100 * AllowGroupCount / TotalBookings > umbral || umbral == 0;
            Activities = 100 * ActivitiesCount / TotalBookings > umbral || umbral == 0;
            // Podríamos acotar más la promoción por tipos de hotel con un funcionamiento análogo al anterior
        }

        public bool RatingAccepted(Enums.Enums.PromotionLevel nivel, double rating)
        {
            switch (nivel)
            {
                case Enums.Enums.PromotionLevel.Low:
                    return Math.Abs(RatingMean - rating) < 0.5;
                case Enums.Enums.PromotionLevel.Mid:
                    return Math.Abs(RatingMean - rating) < 1;
                case Enums.Enums.PromotionLevel.High:
                    return true; // No discriminamos
            }
            return false;
        }
    }
}
