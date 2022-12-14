using System;
using System.Linq;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    public class WebhookSubscriptionController : Controller
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;
        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{secret}", Name = nameof(GetSubscriptionBySecret))]
        public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionBySecret(string secret)
        {
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.Secret.ToLower() == secret.ToLower());
            if (subscription is null)
            {
                return NotFound();
            }
            else
            {
                var result = _mapper.Map<WebhookSubscriptionReadDto>(subscription);

                return Ok(result);
            }

        }

        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscriptionCreateDto)
        {
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.WebhookURI.ToLower() == webhookSubscriptionCreateDto.WebhookURI.ToLower());
            if (subscription is null)
            {
                subscription = _mapper.Map<WebhookSubscription>(webhookSubscriptionCreateDto);
                subscription.Secret = Guid.NewGuid().ToString();
                subscription.WebhookPublisher = "PanAus";

                try
                {
                    _context.WebhookSubscriptions.Add(subscription);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }

                var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(subscription);

                return CreatedAtRoute(nameof(GetSubscriptionBySecret), new { secret = webhookSubscriptionReadDto.Secret }, webhookSubscriptionReadDto);
            }
            else
            {
                return NoContent();
            }
        }
    }
}