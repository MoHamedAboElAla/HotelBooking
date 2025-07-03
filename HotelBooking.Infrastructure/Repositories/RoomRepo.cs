using HotelBooking.Domain.IRepositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories
{
    public class RoomRepo : IRoomRepo
    {
        private readonly AppDbContext _context;

        public RoomRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.Include(r => r.Hotel).ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms.Include(r => r.Hotel).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public void Update(Room room)
        {
            _context.Rooms.Update(room);
        }

        public void Delete(Room room)
        {
            _context.Rooms.Remove(room);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
