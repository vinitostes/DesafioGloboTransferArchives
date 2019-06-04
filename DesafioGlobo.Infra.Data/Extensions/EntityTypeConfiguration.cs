using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Infra.Data.Extensions
{
    public abstract class EntityTypeConfiguration<T> where T : class
    {
        public abstract void Map(EntityTypeBuilder<T> builder);
    }
}
