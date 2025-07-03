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
    public class HotelRepo : Repo<Hotel>, IHotelRepo
    {
        private readonly AppDbContext _context;
        public HotelRepo(AppDbContext context) :base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Hotel>> GetTopRatedHotelsAsync()
        {
            return await _context.Hotels.Where(h => h.Stars >= 4).ToListAsync();
        }
    }
}
