using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PavelNikolaevKt_31_23.Database.Helpers;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const string TableName = "cd_student";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.StudentId);
            builder.Property(p => p.StudentId).ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(100);

            builder.Property(p => p.LastName)
                   .HasColumnType(ColumnType.String)
                   .HasMaxLength(100);

            builder.Property(p => p.IsDeleted)
                   .HasColumnType(ColumnType.Bool)
                   .HasDefaultValue(false);

            // Связь: Group 1 -> many Student
            builder.HasOne(p => p.Group)
                   .WithMany()
                   .HasForeignKey(p => p.GroupId)
                   .HasConstraintName("fk_f_group_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");

            builder.Navigation(p => p.Group).AutoInclude();
        }
    }
}