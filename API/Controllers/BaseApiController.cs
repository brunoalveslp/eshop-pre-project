using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // controller must by inset [] and not {}
public class BaseApiController : ControllerBase
{
}
