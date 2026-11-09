using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetGroupsAsync(GroupFilter filter, CancellationToken token);
        Task<Group> AddGroupAsync(Group group, CancellationToken token);
        Task<Group?> UpdateGroupAsync(int id, Group group, CancellationToken token);
        Task<bool> DeleteGroupAsync(int id, CancellationToken token);
    }
}
