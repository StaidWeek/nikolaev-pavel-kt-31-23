using Microsoft.AspNetCore.Mvc;
using PavelNikolaevKt_31_23.Filters;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService) => _studentService = studentService;

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] StudentFilter filter, CancellationToken token)
            => Ok(await _studentService.GetStudentsAsync(filter, token));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Student student, CancellationToken token)
            => Ok(await _studentService.AddStudentAsync(student, token));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student, CancellationToken token)
        {
            var result = await _studentService.UpdateStudentAsync(id, student, token);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
            => await _studentService.DeleteStudentAsync(id, token) ? Ok() : NotFound();
    }
}
