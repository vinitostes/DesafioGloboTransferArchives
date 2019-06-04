using DesafioGlobo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Infra.Data.Repository.Interfaces
{
    public interface ITransferArchiveControlRepository : IRepository<TransferArchiveControl>
    {
        ICollection<TransferArchiveControl> GetTransferArchivesByProcess();

        TransferArchiveControl GetTransferArchivesControlById(Guid id);
    }
}
