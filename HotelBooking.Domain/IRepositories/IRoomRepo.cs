using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.IRepositories
{
    public interface IRoomRepo
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task AddAsync(Room room);
        void Update(Room room);
        void Delete(Room room);
        Task SaveChangesAsync();
    }
}
