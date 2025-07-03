using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.IRepositories
{
    public interface IHotelRepo :IRepo<Hotel>
    {
        Task<IEnumerable<Hotel>> GetTopRatedHotelsAsync();
    }
}
