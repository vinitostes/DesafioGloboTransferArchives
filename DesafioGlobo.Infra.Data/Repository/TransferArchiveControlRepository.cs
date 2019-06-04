using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Infra.Data.Context;
using DesafioGlobo.Infra.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioGlobo.Infra.Data.Repository
{
    public class TransferArchiveControlRepository : Repository<TransferArchiveControl>, ITransferArchiveControlRepository
    {
        public TransferArchiveControlRepository(ContextBase contextBase) : base(contextBase)
        {

        }

        public ICollection<TransferArchiveControl> GetTransferArchivesByProcess()
        {

            var query = Db.transferArchiveControl.Where(
                    x => (x.IdResponse == null || x.IdResponse.Equals(string.Empty)))
                    .OrderBy(x => x.CreateDate)
                    .Take(100);

            return query.ToList();
        }

        public TransferArchiveControl GetTransferArchivesControlById(Guid id)
        {
            var query = Db.transferArchiveControl.Where(
                    x => (x.IdTransferArchiveControl == id)
                );

            return query.FirstOrDefault();
        }
    }
}
