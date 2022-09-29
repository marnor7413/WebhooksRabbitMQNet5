using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class Controller : ControllerBase
    {
        // no implementation
    }
}