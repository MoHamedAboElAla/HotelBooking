using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.IRepositories
{
    public interface IHotelRepo 
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel?> GetByIdAsync(int id);
        Task AddAsync(Hotel entity);
        void Update(Hotel entity);
        void Delete(Hotel entity);
        Task SaveAsync();
        Task<IEnumerable<Hotel>> GetTopRatedHotelsAsync();
    }
}
