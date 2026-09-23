using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PavelNikolaevKt_31_23.Database.Helpers;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Database.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        private const string TableName = "cd_specialty";

        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable(TableName); // без схемы!

            builder.HasKey(p => p.SpecialtyId);
            builder.Property(p => p.SpecialtyId).ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(200);

            builder.Property(p => p.Code)
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(50);
        }
    }
}