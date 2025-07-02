using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        public async Task<IActionResult> Index()
        {
            var seasons = await _seasonService.GetAllSeasonsAsync();
            return View(seasons);
        }

        public async Task<IActionResult> Details(int id)
        {
            var season = await _seasonService.GetSeasonByIdAsync(id);
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

            await _seasonService.AddSeasonAsync(season);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var season = await _seasonService.GetSeasonByIdAsync(id);
            if (season == null) return NotFound();

            return View(season);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Season season)
        {
            if (!ModelState.IsValid) return View(season);

            await _seasonService.UpdateSeasonAsync(season);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var season = await _seasonService.GetSeasonByIdAsync(id);
            if (season == null) return NotFound();

            return View(season);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _seasonService.DeleteSeasonAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}