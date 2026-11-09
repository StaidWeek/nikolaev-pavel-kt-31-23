using Microsoft.EntityFrameworkCore;
using PavelNikolaevKt_31_23.Database;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Services
{
    public class GradeService : IGradeService
    {
        private readonly UniversityDbContext _context;

        public GradeService(UniversityDbContext context) => _context = context;

        // 1. Средний балл по предмету в группе
        public async Task<double> GetAverageGradeBySubjectInGroupAsync(int disciplineId, int groupId, CancellationToken token)
        {
            var avg = await _context.Grades
                .Where(g => g.DisciplineId == disciplineId && g.Student != null && g.Student.GroupId == groupId)
                .AverageAsync(g => (double?)g.Value, token);
            return avg ?? 0;
        }

        // 2. Оценка конкретного студента по предмету
        public async Task<int?> GetStudentGradeBySubjectAsync(int studentId, int disciplineId, CancellationToken token)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.DisciplineId == disciplineId, token);
            return grade?.Value;
        }

        // 3. Средний балл по году (курсу)
        public async Task<double> GetAverageGradeByCourseAsync(int course, CancellationToken token)
        {
            var avg = await _context.Grades
                .Where(g => g.Student != null && g.Student.Group != null && g.Student.Group.Course == course)
                .AverageAsync(g => (double?)g.Value, token);
            return avg ?? 0;
        }

        public async Task<Grade> AddGradeAsync(Grade grade, CancellationToken token)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync(token);
            return grade;
        }

        public async Task<Grade?> UpdateGradeAsync(int id, int newValue, CancellationToken token)
        {
            var grade = await _context.Grades.FindAsync(new object[] { id }, token);
            if (grade == null) return null;

            grade.Value = newValue;
            await _context.SaveChangesAsync(token);
            return grade;
        }
    }
}
