using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PavelNikolaevKt_31_23.Database.Helpers;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "cd_grade";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.GradeId);
            builder.Property(p => p.GradeId).ValueGeneratedOnAdd();

            builder.Property(p => p.Value)
                   .HasColumnType(ColumnType.Int);

            // Связь: Student 1 -> many Grade
            builder.HasOne(p => p.Student)
                   .WithMany()
                   .HasForeignKey(p => p.StudentId)
                   .HasConstraintName("fk_f_student_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_student_id");

            // Связь: Discipline 1 -> many Grade
            builder.HasOne(p => p.Discipline)
                   .WithMany()
                   .HasForeignKey(p => p.DisciplineId)
                   .HasConstraintName("fk_f_discipline_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");

            builder.Navigation(p => p.Student).AutoInclude();
            builder.Navigation(p => p.Discipline).AutoInclude();
        }
    }
}