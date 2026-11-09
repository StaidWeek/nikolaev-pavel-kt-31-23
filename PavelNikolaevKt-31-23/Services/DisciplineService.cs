using Microsoft.EntityFrameworkCore;
using PavelNikolaevKt_31_23.Database;
using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly UniversityDbContext _context;

        public DisciplineService(UniversityDbContext context) => _context = context;

        public async Task<List<Discipline>> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken token)
        {
            var query = _context.Disciplines.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(d => d.Name != null && d.Name.Contains(filter.Name));

            if (filter.IsDeleted.HasValue)
                query = query.Where(d => d.IsDeleted == filter.IsDeleted.Value);

            return await query.ToListAsync(token);
        }

        public async Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken token)
        {
            _context.Disciplines.Add(discipline);
            await _context.SaveChangesAsync(token);
            return discipline;
        }

        public async Task<Discipline?> UpdateDisciplineAsync(int id, Discipline discipline, CancellationToken token)
        {
            var existing = await _context.Disciplines.FindAsync(new object[] { id }, token);
            if (existing == null) return null;

            existing.Name = discipline.Name;
            existing.IsDeleted = discipline.IsDeleted;

            await _context.SaveChangesAsync(token);
            return existing;
        }

        public async Task<bool> DeleteDisciplineAsync(int id, CancellationToken token)
        {
            var discipline = await _context.Disciplines.FindAsync(new object[] { id }, token);
            if (discipline == null) return false;

            discipline.IsDeleted = true;
            await _context.SaveChangesAsync(token);
            return true;
        }
    }
}
