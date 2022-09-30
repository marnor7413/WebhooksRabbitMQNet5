using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelAgentWeb.Data;
using TravelAgentWeb.Dtos;

namespace TravelAgentWeb.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly TravelAgentDbContext _context;

        public NotificationsController(TravelAgentDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult FlightChanged(FlightDetailUpdateDto flightDetailUpdateDto)
        {
            Console.WriteLine($"Webhook received from {flightDetailUpdateDto.Publisher}.");

            var data = _context.SubscriptionSecrets.FirstOrDefault(s =>
                s.Publisher == flightDetailUpdateDto.Publisher
                && s.Secret == flightDetailUpdateDto.Secret
            );

            if (data is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid secret");
                Console.ResetColor();
                return BadRequest();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Old price {flightDetailUpdateDto.OldPrice}.");
            Console.ResetColor();

            return Ok();
        }
    }
}