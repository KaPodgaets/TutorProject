using Microsoft.AspNetCore.Mvc;
using TutorProject.Domain.Shared;
using TutorProject.Domain.Shared.HttpResponsing;

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