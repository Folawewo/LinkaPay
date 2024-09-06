using System;
namespace LinkaPay.Domain.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task AddSync(T entity);
        Task<T> GetUserByEmail(string email);
        Task AddUser(T entity);
    }
}

