using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Interfaces
{
    public interface ISeasonService
    {
        Task<IEnumerable<Season>> GetAllSeasonsAsync();
        Task<Season?> GetSeasonByIdAsync(int id);
        Task AddSeasonAsync(Season season);
        Task UpdateSeasonAsync(Season season);
        Task DeleteSeasonAsync(int id);
    }
}