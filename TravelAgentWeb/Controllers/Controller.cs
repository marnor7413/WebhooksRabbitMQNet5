using Microsoft.AspNetCore.Mvc;

namespace TravelAgentWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class Controller : ControllerBase
    {
        // no implementation
    }
}