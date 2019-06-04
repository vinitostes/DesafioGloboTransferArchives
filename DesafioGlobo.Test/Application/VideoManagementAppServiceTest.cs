using AutoMapper;
using DesafioGlobo.Application.AutoMapper;
using DesafioGlobo.Application.Interfaces;
using DesafioGlobo.Application.Services;
using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Infra.CrossCutting.Helper.SendEmail;
using DesafioGlobo.Infra.CrossCutting.IoC;
using DesafioGlobo.Infra.Data.Context;
using DesafioGlobo.Infra.Data.Repository;
using DesafioGlobo.Infra.Data.Repository.Interfaces;
using DesafioGlobo.Infra.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.IO;

namespace DesafioGlobo.Test
{
    public class VideoManagementAppServiceTest : BaseTest
    {

        [TestCase("C:\\DesafioGlobo\\Video1.mp4", "C:\\DesafioGlobo\\TesteCopia")]
        public void CopyArchiveTest(string fullName, string pathFolder)
        {
            // Arrange
            var checksum = _videoManagementAppService.Checksum(fullName);
            var transferArchive = new TransferArchive(fullName, pathFolder);

            // Act
            var result = _videoManagementAppService.CopyArchive(transferArchive);

            // Assert
            Assert.AreEqual(result.CheckSum, checksum);
        }

        [TestCase("C:\\DesafioGlobo\\TesteCopia\\Video1.mp4")]
        public void DeleteArchiveTest(string fullName)
        {
            // Arrange
            FileInfo file = new FileInfo(fullName);
            var transferArchive = new TransferArchive(fullName,string.Empty);

            // Act
            _videoManagementAppService.DeleteArchive(transferArchive);
            var result = file.Exists;

            // Assert
            Assert.False(result);
        }

        [TestCase("C:\\DesafioGlobo\\Video1.mp4", "C:\\DesafioGlobo\\TesteCopia")]
        public void MoveArchiveTest(string fullName, string pathFolder)
        {
            // Arrange
            var checksum = _videoManagementAppService.Checksum(fullName);
            var transferArchive = new TransferArchive(fullName, pathFolder);

            // Act
            var result = _videoManagementAppService.MoveArchive(transferArchive);

            // Assert
            Assert.AreEqual(result.CheckSum, checksum);
        }
    }
}