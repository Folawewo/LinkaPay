using System.Threading.Tasks;
using LinkaPay.Domain.Entities;
using LinkaPay.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkaPay.Infrastructure.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetUserByEmail(string email)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(u => ((Users)(object)u).EmailAddress == email);
        }

        public async Task AddUser(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
