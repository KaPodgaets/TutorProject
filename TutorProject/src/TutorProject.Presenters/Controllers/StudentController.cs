using Microsoft.AspNetCore.Mvc;
using TutorProject.Domain.Shared;

namespace TutorProject.Presenters.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ApplicationController
{
    [HttpGet]
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetStudentById()
    {
        await Task.CompletedTask;

        return Ok("result");
    }
}