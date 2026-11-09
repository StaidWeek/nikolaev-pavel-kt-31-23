using Microsoft.EntityFrameworkCore;
using PavelNikolaevKt_31_23.Database;
using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Services
{
    public class GroupService : IGroupService
    {
        private readonly UniversityDbContext _context;

        public GroupService(UniversityDbContext context) => _context = context;

        public async Task<List<Group>> GetGroupsAsync(GroupFilter filter, CancellationToken token)
        {
            var query = _context.Groups.AsQueryable();

            if (filter.SpecialtyId.HasValue)
                query = query.Where(g => g.SpecialtyId == filter.SpecialtyId.Value);

            if (filter.Course.HasValue)
                query = query.Where(g => g.Course == filter.Course.Value);

            if (filter.IsDeleted.HasValue)
                query = query.Where(g => g.IsDeleted == filter.IsDeleted.Value);

            return await query.Include(g => g.Specialty).ToListAsync(token);
        }

        public async Task<Group> AddGroupAsync(Group group, CancellationToken token)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync(token);
            return group;
        }

        public async Task<Group?> UpdateGroupAsync(int id, Group group, CancellationToken token)
        {
            var existing = await _context.Groups.FindAsync(new object[] { id }, token);
            if (existing == null) return null;

            existing.Name = group.Name;
            existing.Course = group.Course;
            existing.SpecialtyId = group.SpecialtyId;
            existing.IsDeleted = group.IsDeleted;

            await _context.SaveChangesAsync(token);
            return existing;
        }

        public async Task<bool> DeleteGroupAsync(int id, CancellationToken token)
        {
            var group = await _context.Groups.FindAsync(new object[] { id }, token);
            if (group == null) return false;

            group.IsDeleted = true;

            // При удалении группы студенты тоже удаляются (мягкое удаление)
            var students = await _context.Students.Where(s => s.GroupId == id).ToListAsync(token);
            foreach (var student in students)
            {
                student.IsDeleted = true;
            }

            await _context.SaveChangesAsync(token);
            return true;
        }
    }
}
