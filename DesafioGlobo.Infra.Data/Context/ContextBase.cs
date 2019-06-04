using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Infra.Data.Extensions;
using DesafioGlobo.Infra.Data.MappingConfig;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioGlobo.Infra.Data.Context
{
    public class ContextBase : DbContext
    {
        public IConfigurationRoot Configuration { get; set; }

        public ContextBase()
        {

        }

        public ContextBase(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<TransferArchiveControl> transferArchiveControl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer(config.GetConnectionString("DefaultConnection"), providerOptions => providerOptions.CommandTimeout(60));
                    //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<ValidationFailure>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.AddConfiguration(new TransferArchiveControlMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
