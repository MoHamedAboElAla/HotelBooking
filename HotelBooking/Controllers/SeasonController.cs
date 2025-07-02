using HotelBooking.Domain.IRepositories;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeasonRepo _seasonRepo;

        public SeasonController(ISeasonRepo seasonRepo)
        {
            _seasonRepo = seasonRepo;
        }

        public async Task<IActionResult> Index()
        {
            var seasons = await _seasonRepo.GetAllAsync();
            return View(seasons);
        }

        public async Task<IActionResult> Details(int id)
        {
            var season = await _seasonRepo.GetByIdAsync(id);
            if (season == null) return NotFound();

            return View(season);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Season season)
        {
            if (!ModelState.IsValid) return View(season);

            await _seasonRepo.AddAsync(season);
            await _seasonRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var season = await _seasonRepo.GetByIdAsync(id);
            if (season == null) return NotFound();

            return View(season);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Season season)
        {
            if (!ModelState.IsValid) return View(season);

            _seasonRepo.Update(season);
            await _seasonRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var season = await _seasonRepo.GetByIdAsync(id);
            if (season == null) return NotFound();

            return View(season);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var season = await _seasonRepo.GetByIdAsync(id);
            if (season != null)
            {
                _seasonRepo.Delete(season);
                await _seasonRepo.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}