using DesafioGlobo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioGlobo.Infra.Data.Repository.Interfaces
{
    public interface IVideoManagementRepository
    {
        ICollection<FileInfo> GetAllArchive(FindArchive archive);

        ICollection<FileInfo> GetArchivesByPartialName(FindArchive archive);

        void MoveArchive(TransferArchive archive);

        void DeleteArchive(TransferArchive archive);

        void CopyArchive(TransferArchive archive);

        void FtpUploadArchive(TransferArchiveFtp archiveFtp);
    }
}
