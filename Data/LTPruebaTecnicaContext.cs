using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Data
{
    public class LTPruebaTecnicaContext : DbContext
    {
        public LTPruebaTecnicaContext (DbContextOptions<LTPruebaTecnicaContext> options)
            : base(options)
        {
        }        

        public DbSet<PruebaTecnica.Models.HotelType> HotelType { get; set; }

        public DbSet<PruebaTecnica.Models.Country> Country { get; set; }

        public DbSet<PruebaTecnica.Models.Customer> Customer { get; set; }

        public DbSet<PruebaTecnica.Models.Hotel> Hotel { get; set; }

        public DbSet<PruebaTecnica.Models.Booking> Booking { get; set; }


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
            .HasMany<Hotel>(b => b.Hotels).WithMany(h => h.Bookings);
        }
        #endregion
    }
}
