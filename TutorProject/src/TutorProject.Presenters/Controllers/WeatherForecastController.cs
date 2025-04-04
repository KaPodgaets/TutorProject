using Microsoft.AspNetCore.Mvc;

namespace TutorProject.Presenters.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public void Get()
    {
        Console.WriteLine("Hello World!");
    }
}