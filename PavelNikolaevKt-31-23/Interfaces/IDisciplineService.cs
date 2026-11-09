using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Interfaces
{
    public interface IDisciplineService
    {
        Task<List<Discipline>> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken token);
        Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken token);
        Task<Discipline?> UpdateDisciplineAsync(int id, Discipline discipline, CancellationToken token);
        Task<bool> DeleteDisciplineAsync(int id, CancellationToken token);
    }
}
