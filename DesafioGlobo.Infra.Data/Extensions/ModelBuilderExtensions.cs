using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Infra.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<T>(this ModelBuilder modelBuilder, EntityTypeConfiguration<T> configuration) where T : class
        {
            configuration.Map(modelBuilder.Entity<T>());
        }
    }
}
