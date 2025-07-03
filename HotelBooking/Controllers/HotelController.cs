using HotelBooking.Domain.IRepositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Repositories;
using HotelBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class HotelController : Controller
    {

        private readonly IHotelRepo _hotelRepo;
        public HotelController(IHotelRepo hotelRepo)
        {

            _hotelRepo = hotelRepo;
        }
        public async Task <IActionResult> Index()
        {
            var hotels = await _hotelRepo.GetAllAsync();

            return View(hotels);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound();
            var model = new HotelViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Location = hotel.Location,
                Country = hotel.Country,
                Stars = hotel.Stars,
                Description = hotel.Description,
                ImageUrl = hotel.ImageUrl
            };

            return View(model);
 
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task <IActionResult> Create(HotelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string imageName = null;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                string path = Directory.GetCurrentDirectory();
                string imagePath = "wwwroot/uploads/hotels";
                string uploadsFolder = Path.Combine(path, imagePath);
                imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
            }
            Hotel hotel = new Hotel
            {
                Name = model.Name,
                Location = model.Location,
                Country = model.Country,
                Stars = model.Stars,
                Description = model.Description,
                ImageUrl = imageName
            };

            await _hotelRepo.AddAsync(hotel);
            await _hotelRepo.SaveAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound();
            var viewModel = new HotelViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Location = hotel.Location,
                Country = hotel.Country,
                Stars = hotel.Stars,
                Description = hotel.Description,
                ImageUrl = hotel.ImageUrl
            };

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HotelViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var hotel = await _hotelRepo.GetByIdAsync(model.Id);
            if (hotel == null)
            {
                return NotFound();
            }

            hotel.Name = model.Name;
            hotel.Location = model.Location;
            hotel.Country = model.Country;
            hotel.Stars = model.Stars;
            hotel.Description = model.Description;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                string path = Directory.GetCurrentDirectory();
                string imagePath = "wwwroot/uploads/hotels";
                if (!string.IsNullOrEmpty(hotel.ImageUrl))
                {

                    string oldPath = Path.Combine(path, imagePath, hotel.ImageUrl);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                string newImageName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string newPath = Path.Combine(path, imagePath, newImageName);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                hotel.ImageUrl = newImageName;
            }

            _hotelRepo.Update(hotel);
            await _hotelRepo.SaveAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
                return NotFound();

            return View(hotel);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            _hotelRepo.Delete(hotel!);
            await _hotelRepo.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
