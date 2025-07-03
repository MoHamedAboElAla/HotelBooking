using HotelBooking.Domain.IRepositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories
{
    public class SeasonRepo : ISeasonRepo
    {
        private readonly AppDbContext _context;

        public SeasonRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Season>> GetAllAsync()
        {
            return await _context.Seasons.ToListAsync();
        }

        public async Task<Season?> GetByIdAsync(int id)
        {
            return await _context.Seasons.FindAsync(id);
        }

        public async Task AddAsync(Season season)
        {
            await _context.Seasons.AddAsync(season);
        }

        public void Update(Season season)
        {
            _context.Seasons.Update(season);
        }

        public void Delete(Season season)
        {
            _context.Seasons.Remove(season);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}