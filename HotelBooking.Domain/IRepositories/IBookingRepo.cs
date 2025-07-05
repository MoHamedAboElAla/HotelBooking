using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.IRepositories
{
    public interface IBookingRepo
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task AddAsync(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);
        Task<bool> ExistsAsync(int id);
        Task<List<Booking>> GetBookingsByHotelIdAsync(int hotelId);
        Task<List<Booking>> GetBookingsByRoomIdAsync(int roomId);
        Task SaveChangesAsync();

    }
}
