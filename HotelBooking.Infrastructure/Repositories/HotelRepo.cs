using HotelBooking.Domain.IRepositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Repositories
{
    public class HotelRepo : IHotelRepo
    {
        private readonly AppDbContext _context;
        public HotelRepo(AppDbContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(Hotel entity)
        {
            await _context.AddAsync(entity);
        }

        public void Delete(Hotel entity)
        {
            _context.Hotels.Remove(entity);
        }

        public async  Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return   await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel?> GetByIdAsync(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Hotel entity)
        {
            _context.Update(entity);
        }

        public async Task<IEnumerable<Hotel>> GetTopRatedHotelsAsync()
        {
            return await _context.Hotels.Where(h => h.Stars >= 4).ToListAsync();
        }

      
    }
}
