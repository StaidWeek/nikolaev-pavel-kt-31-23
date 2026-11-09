using Microsoft.AspNetCore.Mvc;
using PavelNikolaevKt_31_23.Interfaces;
using PavelNikolaevKt_31_23.Models;

namespace PavelNikolaevKt_31_23.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradesController(IGradeService gradeService) => _gradeService = gradeService;

        [HttpGet("avg-group")]
        public async Task<IActionResult> GetAvgInGroup(int disciplineId, int groupId, CancellationToken token)
            => Ok(await _gradeService.GetAverageGradeBySubjectInGroupAsync(disciplineId, groupId, token));

        [HttpGet("student")]
        public async Task<IActionResult> GetStudentGrade(int studentId, int disciplineId, CancellationToken token)
            => Ok(await _gradeService.GetStudentGradeBySubjectAsync(studentId, disciplineId, token));

        [HttpGet("avg-course")]
        public async Task<IActionResult> GetAvgByCourse(int course, CancellationToken token)
            => Ok(await _gradeService.GetAverageGradeByCourseAsync(course, token));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Grade grade, CancellationToken token)
            => Ok(await _gradeService.AddGradeAsync(grade, token));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] int newValue, CancellationToken token)
        {
            var result = await _gradeService.UpdateGradeAsync(id, newValue, token);
            return result == null ? NotFound() : Ok(result);
        }
    }
}