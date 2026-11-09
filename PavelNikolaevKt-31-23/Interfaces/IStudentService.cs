using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync(StudentFilter filter, CancellationToken token);
        Task<Student> AddStudentAsync(Student student, CancellationToken token);
        Task<Student?> UpdateStudentAsync(int id, Student student, CancellationToken token);
        Task<bool> DeleteStudentAsync(int id, CancellationToken token);
    }
}