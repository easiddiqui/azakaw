using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azakaw.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {

    }
}