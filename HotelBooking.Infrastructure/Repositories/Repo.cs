using HotelBooking.Domain.IRepositories;
using HotelBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Repositories
{
    public class Repo<T> :IRepo<T> where T :class
    {
        private readonly AppDbContext _context;

        private readonly DbSet<T> _dbset;

        public Repo(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbset.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _dbset.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
    }
}
