using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Interfaces
{
    public interface IGradeService
    {
        Task<double> GetAverageGradeBySubjectInGroupAsync(int disciplineId, int groupId, CancellationToken token);
        Task<int?> GetStudentGradeBySubjectAsync(int studentId, int disciplineId, CancellationToken token);
        Task<double> GetAverageGradeByCourseAsync(int course, CancellationToken token);
        Task<Grade> AddGradeAsync(Grade grade, CancellationToken token);
        Task<Grade?> UpdateGradeAsync(int id, int newValue, CancellationToken token);
    }
}
