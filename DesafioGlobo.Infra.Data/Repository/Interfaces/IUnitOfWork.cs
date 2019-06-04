using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesafioGlobo.Infra.Data.Repository.Interfaces
{
    public interface IUnitOfwork : IDisposable
    {
        void Commit();
    }
}
