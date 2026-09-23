using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PavelNikolaevKt_31_23.Database.Helpers;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.GroupId);
            builder.Property(p => p.GroupId).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(100);

            builder.Property(p => p.Course)
                   .HasColumnType(ColumnType.Int);

            builder.Property(p => p.IsDeleted)
                   .HasColumnType(ColumnType.Bool)
                   .HasDefaultValue(false); // в SQLite допустимо

            // Связь: Specialty 1 -> many Group
            builder.HasOne(p => p.Specialty)
                   .WithMany()
                   .HasForeignKey(p => p.SpecialtyId)
                   .HasConstraintName("fk_f_specialty_id")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.SpecialtyId, $"idx_{TableName}_fk_f_specialty_id");

            builder.Navigation(p => p.Specialty).AutoInclude();
        }
    }
}