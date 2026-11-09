using Microsoft.EntityFrameworkCore;
using PavelNikolaevKt_31_23.Database;
using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Services
{
    public class StudentService : IStudentService
    {
        private readonly UniversityDbContext _context;

        public StudentService(UniversityDbContext context) => _context = context;

        public async Task<List<Student>> GetStudentsAsync(StudentFilter filter, CancellationToken token)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
                query = query.Where(s => s.Group != null && s.Group.Name != null && s.Group.Name == filter.GroupName);

            if (!string.IsNullOrWhiteSpace(filter.FullName))
                query = query.Where(s => (s.FirstName != null && s.FirstName.Contains(filter.FullName)) ||
                                         (s.LastName != null && s.LastName.Contains(filter.FullName)));

            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted.Value);

            return await query.Include(s => s.Group).ToListAsync(token);
        }

        public async Task<Student> AddStudentAsync(Student student, CancellationToken token)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync(token);
            return student;
        }

        public async Task<Student?> UpdateStudentAsync(int id, Student student, CancellationToken token)
        {
            var existing = await _context.Students.FindAsync(new object[] { id }, token);
            if (existing == null) return null;

            existing.FirstName = student.FirstName;
            existing.LastName = student.LastName;
            existing.GroupId = student.GroupId;
            existing.IsDeleted = student.IsDeleted;

            await _context.SaveChangesAsync(token);
            return existing;
        }

        public async Task<bool> DeleteStudentAsync(int id, CancellationToken token)
        {
            var student = await _context.Students.FindAsync(new object[] { id }, token);
            if (student == null) return false;

            student.IsDeleted = true;
            await _context.SaveChangesAsync(token);
            return true;
        }
    }
}
