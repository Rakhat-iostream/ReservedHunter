using StudentPositionHunters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPositionHunters.Repositories
{
    public interface IEntityRepository<T>
    {
        public Task<bool> Create(T entity);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(T entity);
        public Task<bool> HasEntity(T entity);
        public Task<T> GetById(int id);
        public Task<IList<T>> GetAll();
    }
}
