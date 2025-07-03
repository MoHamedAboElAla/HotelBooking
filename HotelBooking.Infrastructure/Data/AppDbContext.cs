using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
         public DbSet<Hotel> Hotels { get; set; }
         public DbSet<Room> Rooms { get; set; }
         public DbSet<Booking> Bookings { get; set; }
         public DbSet<Agent> Agents { get; set; }
         public DbSet<Season> Seasons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.RoomNumber, r.HotelId })
                .IsUnique();
        }
    }

}
