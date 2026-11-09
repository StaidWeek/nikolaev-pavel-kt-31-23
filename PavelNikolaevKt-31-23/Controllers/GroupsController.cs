using Microsoft.AspNetCore.Mvc;
using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService) => _groupService = groupService;

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] GroupFilter filter, CancellationToken token)
            => Ok(await _groupService.GetGroupsAsync(filter, token));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Group group, CancellationToken token)
            => Ok(await _groupService.AddGroupAsync(group, token));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Group group, CancellationToken token)
        {
            var result = await _groupService.UpdateGroupAsync(id, group, token);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
            => await _groupService.DeleteGroupAsync(id, token) ? Ok() : NotFound();
    }
}
