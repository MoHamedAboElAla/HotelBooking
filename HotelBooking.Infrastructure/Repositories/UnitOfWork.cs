//using HotelBooking.Domain.IRepositories;
//using HotelBooking.Infrastructure.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HotelBooking.Infrastructure.Repositories
//{
//    public class UnitOfWork : IUnitOfWork
//    {

//        private readonly AppDbContext _context;

//        public IHotelRepo Hotels { get; }

//        public UnitOfWork(AppDbContext context)
//        {
//            _context = context;
//            Hotels = new HotelRepo(_context);
//        }
//        public async Task SaveAsync()
//        {
//            await _context.SaveChangesAsync();
//        }
//    }
//}
