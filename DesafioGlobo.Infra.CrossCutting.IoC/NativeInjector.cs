using AutoMapper;
using DesafioGlobo.Application.Interfaces;
using DesafioGlobo.Application.Services;
using DesafioGlobo.Infra.CrossCutting.Helper.SendEmail;
using DesafioGlobo.Infra.Data.Context;
using DesafioGlobo.Infra.Data.Repository;
using DesafioGlobo.Infra.Data.Repository.Interfaces;
using DesafioGlobo.Infra.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DesafioGlobo.Infra.CrossCutting.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IVideoManagementAppService, VideoManagementAppService>();


            services.AddScoped<IVideoManagementRepository, VideoManagementRepository>();
            services.AddScoped<IUnitOfwork, UnitOfWork>();
            services.AddScoped<ITransferArchiveControlRepository, TransferArchiveControlRepository>();
            services.AddScoped<ContextBase>();

            services.AddScoped<IMail, Mail>();
            
        }
    }
}
