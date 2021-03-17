using PruebaTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PruebaTecnica.Data;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        private readonly LTPruebaTecnicaContext _context;

        public HomeController(LTPruebaTecnicaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["OutputPromocion"] = Promocionar(HotelesPromocionablesPorCliente());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<string> Promocionar(Dictionary<Customer, List<Hotel>> hotelesPromocionar) {
            // Esta podría ser la única función a ejecutar en este proceso si se almacenase en una vista o tabla "hotelesPromocionar" y se actualizase según se realicen reservas
            // Depende del tiempo de respuesta esperado para la herramienta que dado que está pensada para utilizarse en momentos puntuales, no se interpreta como un problema
            List<string> lstRet = new();
            foreach (Customer c in hotelesPromocionar.Keys) {
                Preferencias p = ObtenerPreferenciasCliente(hotelesPromocionar[c]);
                if (c.PromotionLevel == Models.Enums.Enums.PromotionLevel.Zero)
                {
                    hotelesPromocionar[c] = new();
                    lstRet.Add("El cliente " + c.Name + " no desea recibir promociones.");
                    break;
                }
                else
                {// Filtramos según las preferencias del usuario (en base a su historial de reservas)
                    p.SetPreferencias(c.PromotionLevel);
                    hotelesPromocionar[c] = hotelesPromocionar[c].Where(h => (h.AllowGroups = p.AllowGroup || !p.AllowGroup)
                                                                                && (h.OnlyAdults = p.OnlyAdults || !p.OnlyAdults)
                                                                                && (h.Activities = p.Activities || !p.Activities)
                                                                                && p.RatingAccepted(c.PromotionLevel, h.Rating)).ToList();
                    string strAux = c.RecieveSms ? "Enviar SMS a " + c.Name : "Enviar e-Mail a " + c.Name;
                    foreach (var hot in hotelesPromocionar[c]) {
                        lstRet.Add(strAux + ": " + hot.HotelType.Template.Replace("#nombreHotel#", hot.Name).Replace("#descripcionHotel#", hot.Description).Replace("#nombreCliente#", c.Name) + " en " + hot.Country.Name);
                    }
                }
            }            
            return lstRet;
        }


        private Preferencias ObtenerPreferenciasCliente(List<Hotel> hotelesCliente) {
            return new Preferencias() {
                TotalBookings = hotelesCliente.Count,
                RatingMean = hotelesCliente.Average(h => h.Rating),
                OnlyAdultsCount = hotelesCliente.Where(h => h.OnlyAdults).ToList().Count,
                AllowGroupCount = hotelesCliente.Where(h => h.AllowGroups).ToList().Count,
                ActivitiesCount = hotelesCliente.Where(h => h.Activities).ToList().Count,
            };
        }

        private Dictionary<Customer, List<Hotel>> HotelesPromocionablesPorCliente() {
            // Devuelve un diccionario donde la clave es un cliente y el valor es la lista de hoteles de los paises en los que ha reservado
            Dictionary<Customer, List<int>> paisesPorCliente = ObtenerPaisesConReservasPorCliente();
            Dictionary<Customer, List<Hotel>> hotelesPromocionar = new ();
            foreach (var cli in paisesPorCliente.Keys) {
                hotelesPromocionar.Add(cli, new List<Hotel>());
                hotelesPromocionar[cli].AddRange(ObtenerHotelesFromPaises(paisesPorCliente[cli].Distinct().ToList()));
            }
            return hotelesPromocionar;
        }

        private Dictionary<Customer, List<int>> ObtenerPaisesConReservasPorCliente() {
            // Devuelve un diccionario donde la clave es un cliente y el valor es la lista de países (id) donde ha reservado
            List<Booking> reservas = _context.Booking.Include(b => b.Customer).Include(b => b.Hotels).ToList();
            Dictionary<Customer, List<int>> ret = new ();
            foreach (Booking r in reservas)
            {
                Customer c = r.Customer;
                var paisesReserva = r.Hotels.Select(h => h.CountryId);
                if (!ret.ContainsKey(c))
                {
                    ret.Add(c, new List<int>());
                }
                ret[c].AddRange(paisesReserva);
            }
            return ret;
        }

        private List<Hotel> ObtenerHotelesFromPaises(List<int> countryIds) {
            return _context.Hotel.Include(b => b.Country).Include(b => b.Bookings).Include(b => b.HotelType).Where(h => countryIds.Contains(h.CountryId)).ToList();
        }
    }
}
