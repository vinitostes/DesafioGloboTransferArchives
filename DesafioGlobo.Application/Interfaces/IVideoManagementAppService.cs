using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DesafioGlobo.Application.Interfaces
{
    public interface IVideoManagementAppService
    {
        void GetTransferArchivesByProcess();

        ICollection<FileInfo> GetAllArchive(FindArchive archive);

        ICollection<FileInfo> GetArchivesByPartialName(FindArchive archive);

        TransferArchiveResponse MoveArchive(TransferArchive archive);

        void DeleteArchive(TransferArchive archive);

        TransferArchiveResponse CopyArchive(TransferArchive archive);

        TransferArchiveResponse DeliverByFTP(TransferArchiveFtp archive);

        string Checksum(string file);
    }
}
