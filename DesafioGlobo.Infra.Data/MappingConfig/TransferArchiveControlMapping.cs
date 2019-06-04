using DesafioGlobo.Domain.Entities;
using DesafioGlobo.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGlobo.Infra.Data.MappingConfig
{
    public class TransferArchiveControlMapping : EntityTypeConfiguration<TransferArchiveControl>
    {
        public override void Map(EntityTypeBuilder<TransferArchiveControl> builder)
        {
            builder
                .Property(x => x.IdTransferArchiveControl)
                .IsRequired();

            builder
                .Property(x => x.TypeAction)
                .IsRequired();

            builder
                .Property(x => x.Request)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder
                .Property(x => x.CreateDate)
                .IsRequired();

            builder
                .Property(x => x.IdResponse)
                .IsRequired(false);

            builder
                .Property(x => x.CheckSum)
                .HasColumnType("varchar(150)")
                .IsRequired(false);

            builder
                .Property(x => x.DateSend)
                .IsRequired(false);

            builder
                .Ignore(x => x.ValidationResult);

            builder
                .Ignore(x => x.CascadeMode);

            builder
                .ToTable("TransferArchiveControl");

            builder
                .HasKey(x => x.IdTransferArchiveControl);
        }
    }
}
