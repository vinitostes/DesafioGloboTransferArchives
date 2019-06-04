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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DesafioGlobo.Test
{
    public class BaseTest
    {
        internal IVideoManagementAppService _videoManagementAppService;
        internal ITransferArchiveControlRepository _transferArchiveControlRepository;
        internal IUnitOfwork _unitOfwork;
        internal Microsoft.Extensions.Configuration.IConfiguration Configuration;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfigurationProvider>(AutoMapperConfiguration.RegisterMappings());
            services.AddTransient<IUnitOfwork>(s => new UnitOfWork(new ContextBase()));
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IVideoManagementRepository, VideoManagementRepository>();
            services.AddScoped<ITransferArchiveControlRepository, TransferArchiveControlRepository>();
            services.AddScoped<IMail, Mail>();
            services.AddScoped<ContextBase>();

            services.AddScoped<IVideoManagementAppService, VideoManagementAppService>();

            var serviceProvider = services.BuildServiceProvider();

            _videoManagementAppService = serviceProvider.GetService<IVideoManagementAppService>();
            _transferArchiveControlRepository = serviceProvider.GetService<ITransferArchiveControlRepository>();
            _unitOfwork = serviceProvider.GetService<IUnitOfwork>();
        }
    }
}