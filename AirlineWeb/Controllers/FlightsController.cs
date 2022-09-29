using System;
using System.Linq;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AirlineDbContext _context;

        public FlightsController(IMapper mapper, AirlineDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{flightCode}", Name = nameof(GetFlightDetailsByCode))]
        public ActionResult<FlightDetailReadDto> GetFlightDetailsByCode(string flightCode)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode.ToLower() == flightCode.ToLower());

            if (flight is null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<FlightDetailReadDto>(flight));
        }

        [HttpPost]
        public ActionResult<FlightDetailReadDto> CreateFlight(FlightDetailCreateDto flightDetailCreateDto)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.FlightCode.ToLower() == flightDetailCreateDto.FlightCode.ToLower());

            if (flight is null)
            {
                var flightDetailModel = _mapper.Map<FlightDetail>(flightDetailCreateDto);

                try
                {
                    _context.FlightDetails.Add(flightDetailModel);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }

                var flightDetailReadDto = _mapper.Map<FlightDetailReadDto>(flightDetailModel);

                return CreatedAtRoute(nameof(GetFlightDetailsByCode), new { flightCode = flightDetailReadDto.FlightCode }, flightDetailReadDto);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFlightDetail(int id, FlightDetailUpdateDto flightDetailUpdateDto)
        {
            var flight = _context.FlightDetails.FirstOrDefault(f => f.Id == id);

            if (flight is null) return NotFound();

            _mapper.Map(flightDetailUpdateDto, flight);
            _context.SaveChanges();

            return NoContent();
        }

    }
}