using HotelBooking.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.ViewModel
{
    public class HotelViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(200, MinimumLength = 10)]
        public string? Name { get; set; }

        [Required, StringLength(300, MinimumLength = 10)]
        public string? Location { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        public string? Country { get; set; }

        [Required, Range(1, 5)]
        public byte Stars { get; set; }

        [Required, StringLength(700, MinimumLength = 20)]
        public string? Description { get; set; }
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? ImageFile { get; set; } 

        public string? ImageUrl { get; set; } 
    }
}
