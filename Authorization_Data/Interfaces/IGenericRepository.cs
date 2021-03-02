using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization_Data.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> DeleteAsync(Guid id);
        public Task<bool> UpdateAsync(T obj);
        public Task CreateAsync(T obj);
    }
}