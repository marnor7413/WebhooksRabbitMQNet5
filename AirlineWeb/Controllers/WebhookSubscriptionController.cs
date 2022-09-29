using AirlineWeb.Data;
using AirlineWeb.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    public class WebhookSubscriptionController : Controller
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