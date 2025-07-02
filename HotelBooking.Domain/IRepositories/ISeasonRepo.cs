using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.IRepositories
{
    public interface ISeasonRepo
    {
        Task<IEnumerable<Season>> GetAllAsync();
        Task<Season?> GetByIdAsync(int id);
        Task AddAsync(Season season);
        void Update(Season season);
        void Delete(Season season);
        Task SaveChangesAsync();
    }
}