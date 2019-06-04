using AutoMapper;
using DesafioGlobo.Application.Interfaces;
using DesafioGlobo.Application.ViewModels;
using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Domain.Entities.Enums;
using DesafioGlobo.Domain.Entities.Response;
using DesafioGlobo.Infra.CrossCutting.Helper.SendEmail;
using DesafioGlobo.Infra.Data.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace DesafioGlobo.Application.Services
{
    public class VideoManagementAppService : IVideoManagementAppService
    {
        private readonly IVideoManagementRepository _videoManagementRepository;
        private readonly IMail _mail;
        private readonly ITransferArchiveControlRepository _transferArchiveControlRepository;
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;

        public VideoManagementAppService(IVideoManagementRepository videoManagementRepository,
                                        IMail mail,
                                        ITransferArchiveControlRepository transferArchiveControlRepository,
                                        IUnitOfwork unitOfwork,
                                        IMapper mapper)
        {
            _videoManagementRepository = videoManagementRepository;
            _mail = mail;
            _transferArchiveControlRepository = transferArchiveControlRepository;
            _unitOfwork = unitOfwork;
            _mapper = mapper;
        }


        public void GetTransferArchivesByProcess()
        {
            try
            {
                var archivesByProcess = _transferArchiveControlRepository.GetTransferArchivesByProcess();

                foreach (var archive in archivesByProcess)
                {
                    TransferArchiveResponse checkSum = null;

                    switch (archive.TypeAction)
                    {
                        case (int)TransferArchiveControlEnum.COPY:
                            checkSum = CopyArchive(_mapper.Map<TransferArchive>(JsonConvert.DeserializeObject<TransferArchiveViewModel>(archive.Request)));
                            break;
                        case (int)TransferArchiveControlEnum.MOVE:
                            checkSum = MoveArchive(_mapper.Map<TransferArchive>(JsonConvert.DeserializeObject<TransferArchiveViewModel>(archive.Request)));
                            break;
                        case (int)TransferArchiveControlEnum.DELETE:
                            DeleteArchive(_mapper.Map<TransferArchive>(JsonConvert.DeserializeObject<TransferArchiveViewModel>(archive.Request)));
                            break;
                        case (int)TransferArchiveControlEnum.TRANSFERBYFTP:
                            checkSum = DeliverByFTP(_mapper.Map<TransferArchiveFtp>(JsonConvert.DeserializeObject<TransferArchiveFtpViewModel>(archive.Request)));
                            break;
                        case (int)TransferArchiveControlEnum.GETALL:
                            GetAllArchive(_mapper.Map<FindArchive>(JsonConvert.DeserializeObject<FindArchiveViewModel>(archive.Request)));
                            break;
                        case (int)TransferArchiveControlEnum.GETPARTIALNAME:
                            GetArchivesByPartialName(_mapper.Map<FindArchive>(JsonConvert.DeserializeObject<FindArchiveViewModel>(archive.Request)));
                            break;
                        default:
                            break;
                    }

                    archive.UpdateData(Guid.NewGuid(), (checkSum != null ? checkSum.CheckSum : String.Empty), DateTime.Now);
                    _transferArchiveControlRepository.Update(archive);
                    _unitOfwork.Commit();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<FileInfo> GetAllArchive(FindArchive archive)
        {
            try
            {
                return _videoManagementRepository.GetAllArchive(archive);
            }
            catch (Exception ex)
            {
                //_mail.SendEmail();
                throw ex;
            }
        }

        public ICollection<FileInfo> GetArchivesByPartialName(FindArchive archive)
        {
            try
            {
                return _videoManagementRepository.GetArchivesByPartialName(archive);
            }
            catch (Exception ex)
            {
                //_mail.SendEmail();
                throw ex;
            }

        }

        public TransferArchiveResponse MoveArchive(TransferArchive archive)
        {
            try
            {
                var cheksum = Checksum(archive.FullName);
                TransferArchiveResponse transferArchiveResponse = new TransferArchiveResponse(cheksum);

                _videoManagementRepository.MoveArchive(archive);

                return transferArchiveResponse;
            }
            catch (Exception ex)
            {
                //_mail.SendEmail();
                throw ex;
            }

        }

        public void DeleteArchive(TransferArchive archive)
        {
            try
            {
                _videoManagementRepository.DeleteArchive(archive);

            }
            catch (Exception ex)
            {
                //_mail.SendEmail();
                throw ex;
            }
        }

        public TransferArchiveResponse CopyArchive(TransferArchive archive)
        {
            try
            {
                var cheksum = Checksum(archive.FullName);
                TransferArchiveResponse transferArchiveResponse = new TransferArchiveResponse(cheksum);

                _videoManagementRepository.CopyArchive(archive);

                return transferArchiveResponse;
            }
            catch (Exception ex)
            {
                //_mail.SendEmail();
                throw ex;
            }
        }

        public TransferArchiveResponse DeliverByFTP(TransferArchiveFtp archive)
        {
            try
            {
                var cheksum = Checksum(archive.FullNameArchive);
                TransferArchiveResponse transferArchiveResponse = new TransferArchiveResponse(cheksum);

                _videoManagementRepository.FtpUploadArchive(archive);

                return transferArchiveResponse;
            }
            catch (Exception ex)
            {
                //_mail.SendEmail();
                throw ex;
            }
        }

        public string Checksum(string file)
        {
            try
            {
                using (FileStream stream = File.OpenRead(file))
                {
                    SHA256Managed sha = new SHA256Managed();

                    byte[] checksum = sha.ComputeHash(stream);

                    return BitConverter.ToString(checksum).Replace("-", String.Empty);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
