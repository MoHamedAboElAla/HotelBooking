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
    public class BookingRepo(AppDbContext _context) : IBookingRepo
    {
        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
           
        }

        public void Delete(Booking booking)
        {

            _context.Bookings.Remove(booking);
           
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings
           .Include(b => b.Hotel)
           .Include(b => b.Room)
           .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByHotelIdAsync(int hotelId)
        {
            return await _context.Bookings
           .Where(b => b.HotelId == hotelId)
           .Include(b => b.Room)
           .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByRoomIdAsync(int roomId)
        {
            return await _context.Bookings
            .Where(b => b.RoomId == roomId)
            .Include(b => b.Hotel)
            .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
           .Include(b => b.Hotel)
           .Include(b => b.Room)
          // .Include(b => b.Agent)
           .FirstOrDefaultAsync(b => b.Id == id);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(Booking booking)
        {
            _context.Bookings.Update(booking);
        }
    }
}
