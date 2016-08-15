using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HomeSite.Models.Repository
{
    public interface IRepository<T, in PrimaryKey> : IDisposable
    {
        IEnumerable<T> GetAll();

        T GetSingle(PrimaryKey Id);

        void Add(T entity);

        void Remove(T entity);

        void Put(T entity, PrimaryKey id);
    }
}
