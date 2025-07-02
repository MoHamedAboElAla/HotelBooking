using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.IRepositories;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepo _seasonRepository;

        public SeasonService(ISeasonRepo seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public async Task<IEnumerable<Season>> GetAllSeasonsAsync()
        {
            return await _seasonRepository.GetAllAsync();
        }

        public async Task<Season?> GetSeasonByIdAsync(int id)
        {
            return await _seasonRepository.GetByIdAsync(id);
        }

        public async Task AddSeasonAsync(Season season)
        {
            await _seasonRepository.AddAsync(season);
            await _seasonRepository.SaveChangesAsync();
        }

        public async Task UpdateSeasonAsync(Season season)
        {
            _seasonRepository.Update(season);
            await _seasonRepository.SaveChangesAsync();
        }

        public async Task DeleteSeasonAsync(int id)
        {
            var season = await _seasonRepository.GetByIdAsync(id);
            if (season is not null)
            {
                _seasonRepository.Delete(season);
                await _seasonRepository.SaveChangesAsync();
            }
        }
    }
}