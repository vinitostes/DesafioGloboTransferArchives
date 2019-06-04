using DesafioGlobo.Domain;
using DesafioGlobo.Infra.Data.Context;
using DesafioGlobo.Infra.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioGlobo.Infra.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity<T>
    {
        protected ContextBase Db;
        protected DbSet<T> DbSet;

        protected Repository(ContextBase context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        public virtual void Insert(T obj)
        {
            DbSet.Add(obj);
        }

        public void Update(T obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Delete(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
