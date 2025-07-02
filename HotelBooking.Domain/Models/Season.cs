using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
    public class Season
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required, Range(50, 200, ErrorMessage = "Price factor must be between 50 and 200.0.")]
        public decimal PriceFactor { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();


    }
}
