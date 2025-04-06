using Microsoft.AspNetCore.Mvc;

namespace Users.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ApplicationController
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        await Task.CompletedTask;

        return Ok("result");
    }

    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        await Task.CompletedTask;

        return Ok("result");
    }
}