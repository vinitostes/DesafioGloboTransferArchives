using DesafioGlobo.Infra.Data.Context;
using DesafioGlobo.Infra.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesafioGlobo.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly ContextBase _context;

        public UnitOfWork(ContextBase context)
        {
            _context = context;
        }

        public void Commit()
        {
            var rowsAffected = _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
