using Microsoft.AspNetCore.Mvc;
using TutorProject.Domain.Shared;

namespace TutorProject.Presenters.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}