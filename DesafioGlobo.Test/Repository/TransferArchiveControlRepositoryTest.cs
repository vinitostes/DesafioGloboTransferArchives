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
using System;
using System.Linq;

namespace DesafioGlobo.Test
{
    public class TransferArchiveControlRepositoryTest : BaseTest
    {

        [Test]
        public void GetTransferArchivesByProcessTest()
        {
            // Act
            var result = _transferArchiveControlRepository.GetTransferArchivesByProcess();

            // Assert
            Assert.False(result.Any());
        }
    }
}