using AirlineWeb.Data;
using AirlineWeb.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        public WebhookSubscriptionController(AirlineDbContext context)
        {
            _context = context;
        }

        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscription)
        {
            return null;
        }
    }
}