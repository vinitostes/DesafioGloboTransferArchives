using System;
using AutoMapper;
using DesafioGlobo.Application.AutoMapper;
using DesafioGlobo.Application.Interfaces;
using DesafioGlobo.Infra.CrossCutting.IoC;
using DesafioGlobo.Infra.Data.Context;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using static DesafioGlobo.Application.AutoMapper.AutoMapperConfiguration;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace DesafioGlobo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection Services;
        public IServiceProvider ServiceProvider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ContextBase>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddDbContext<ContextBase>(
                opt => opt.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);            

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Video Management", Version = "v1" });
            });

            services.AddSingleton<IConfigurationProvider>(RegisterMappings());
            services.AddAutoMapper(typeof(Startup));

            RegisterServices(services);

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfire(x => x.UseMemoryStorage());

            Services = services;
            ServiceProvider = Services.BuildServiceProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "DesafioGlobo Service");
            });

            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                // order defines priority
                // beware that queue names should be lowercase only
                Queues = new[] { "critical", "default", "queuetransferarchives" }
            });

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            BackgroundJobs();

        }

        private void BackgroundJobs()
        {
            var _videoManagement = ServiceProvider.GetService<IVideoManagementAppService>();
            
            RecurringJob.AddOrUpdate("queuetransferarchiveid", () => _videoManagement.GetTransferArchivesByProcess(), "*/2 * * * *", null, "queuetransferarchives");
            //_videoManagement.GetTransferArchivesByProcess();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjector.RegisterServices(services);
        }
    }
}
