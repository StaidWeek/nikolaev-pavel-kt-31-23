using Microsoft.AspNetCore.Mvc;
using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _disciplineService;
        public DisciplinesController(IDisciplineService disciplineService) => _disciplineService = disciplineService;

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] DisciplineFilter filter, CancellationToken token)
            => Ok(await _disciplineService.GetDisciplinesAsync(filter, token));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Discipline discipline, CancellationToken token)
            => Ok(await _disciplineService.AddDisciplineAsync(discipline, token));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Discipline discipline, CancellationToken token)
        {
            var result = await _disciplineService.UpdateDisciplineAsync(id, discipline, token);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
            => await _disciplineService.DeleteDisciplineAsync(id, token) ? Ok() : NotFound();
    }
}