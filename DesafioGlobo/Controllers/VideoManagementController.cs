using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DesafioGlobo.Application.Interfaces;
using DesafioGlobo.Application.ViewModels;
using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Domain.Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DesafioGlobo.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VideoManagementController : ControllerBase
    {
        private readonly IVideoManagementAppService _videoManagementAppService;
        private readonly IMapper _mapper;

        public VideoManagementController(IVideoManagementAppService videoManagementAppService,
                                         IMapper mapper)
        {
            _videoManagementAppService = videoManagementAppService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllArchive")]
        public ICollection<FindArchiveResponseViewModel> GetAllArchive(FindArchiveViewModel archive)
        {
            List<FindArchiveResponse> listFindArchiveResponse = new List<FindArchiveResponse>();

            var files = _videoManagementAppService.GetAllArchive(_mapper.Map<FindArchive>(archive));

            foreach (var item in files)
            {
                FindArchiveResponse findArchiveResponse = new FindArchiveResponse(item.FullName, item.DirectoryName, item.Name, item.CreationTime);

                listFindArchiveResponse.Add(findArchiveResponse);
            }

            return _mapper.Map<List<FindArchiveResponseViewModel>>(listFindArchiveResponse);
        }

        [HttpGet]
        [Route("GetArchivesByPartialName")]
        public ICollection<FindArchiveResponseViewModel> GetArchivesByPartialName(FindArchiveViewModel archive)
        {
            List<FindArchiveResponse> listFindArchiveResponse = new List<FindArchiveResponse>();
            var files = _videoManagementAppService.GetArchivesByPartialName(_mapper.Map<FindArchive>(archive));

            foreach (var item in files)
            {
                FindArchiveResponse findArchiveResponse = new FindArchiveResponse(item.FullName, item.DirectoryName, item.Name, item.CreationTime);

                listFindArchiveResponse.Add(findArchiveResponse);
            }

            return _mapper.Map<List<FindArchiveResponseViewModel>>(listFindArchiveResponse);
        }

        [HttpPost]
        [Route("CopyArchive")]
        public TransferArchiveResponseViewModel CopyArchive([FromBody] TransferArchiveViewModel archive)
        {
            return _mapper.Map<TransferArchiveResponseViewModel>(_videoManagementAppService.CopyArchive(_mapper.Map<TransferArchive>(archive)));
        }

        [HttpPost]
        [Route("MoveArchive")]
        public TransferArchiveResponseViewModel MoveArchive([FromBody] TransferArchiveViewModel archive)
        {
            return _mapper.Map<TransferArchiveResponseViewModel>(_videoManagementAppService.MoveArchive(_mapper.Map<TransferArchive>(archive)));
        }

        [HttpPost]
        [Route("DeleteArchive")]
        public void DeleteArchive([FromBody] TransferArchiveViewModel archive)
        {
            _videoManagementAppService.DeleteArchive(_mapper.Map<TransferArchive>(archive));
        }

        [HttpPost]
        [Route("DeliverByFTP")]
        public TransferArchiveResponseViewModel DeliverByFTP([FromBody] TransferArchiveFtpViewModel archiveFtp)
        {
            return _mapper.Map<TransferArchiveResponseViewModel>(_videoManagementAppService.DeliverByFTP(_mapper.Map<TransferArchiveFtp>(archiveFtp)));
        }
    }
}