using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PavelNikolaevKt_31_23.Database.Helpers;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "cd_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.DisciplineId);
            builder.Property(p => p.DisciplineId).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(200);

            builder.Property(p => p.IsDeleted)
                   .HasColumnType(ColumnType.Bool)
                   .HasDefaultValue(false);
        }
    }
}