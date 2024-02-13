using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Authorize]
public abstract class APIBaseController : ControllerBase
{
}
