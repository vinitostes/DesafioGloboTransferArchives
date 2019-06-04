using DesafioGlobo.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Infra.Data.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity<T>
    {
        void Insert(T obj);

        void Update(T obj);

        void Delete(Guid id);

        int SaveChanges();
    }
}
