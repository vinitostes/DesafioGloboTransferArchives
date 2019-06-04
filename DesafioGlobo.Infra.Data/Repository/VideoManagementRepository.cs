using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Infra.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DesafioGlobo.Infra.Data.Repository
{
    public class VideoManagementRepository : IVideoManagementRepository
    {
        public ICollection<FileInfo> GetAllArchive(FindArchive archive)
        {
            try
            {
                DirectoryInfo dir_files = new DirectoryInfo(archive.PathSource);
                FileInfo[] filesInfo = dir_files.GetFiles("*", SearchOption.TopDirectoryOnly);

                Array.Sort(filesInfo, delegate (FileInfo a, FileInfo b) { return DateTime.Compare(a.CreationTime, b.CreationTime); });

                return filesInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<FileInfo> GetArchivesByPartialName(FindArchive archive)
        {
            try
            {
                DirectoryInfo dir_files = new DirectoryInfo(archive.PathSource);
                FileInfo[] filesInfo = dir_files.GetFiles("*" + archive.PartialName + "*.*", SearchOption.TopDirectoryOnly);

                return filesInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CopyArchive(TransferArchive archive)
        {
            try
            {
                FileInfo file = new FileInfo(archive.FullName);

                if (archive.PathTarget != string.Empty)
                {
                    file.CopyTo(archive.PathTarget + "\\" + file.Name, true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteArchive(TransferArchive archive)
        {
            try
            {
                File.Delete(archive.FullName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void MoveArchive(TransferArchive archive)
        {
            try
            {
                FileInfo file = new FileInfo(archive.FullName);

                if (archive.PathTarget != string.Empty)
                {
                    file.MoveTo(archive.PathTarget + "\\" + file.Name);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void FtpUploadArchive(TransferArchiveFtp archiveFtp)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(archiveFtp.Url + "/" + Path.GetFileName(archiveFtp.FullNameArchive));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(archiveFtp.User, archiveFtp.Password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                var stream = File.OpenRead(archiveFtp.FullNameArchive);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                var reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
