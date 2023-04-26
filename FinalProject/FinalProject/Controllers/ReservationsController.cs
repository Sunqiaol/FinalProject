using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly FinalProjectDBContext _context;

        public ReservationsController(FinalProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<Response>> GetReservations()
        {
            var response = new Response();
            var reservation = await _context.Reservation.ToListAsync();
            response.StatusCode = 404;
            response.statusDescription = "Not Found";
            if (_context.Reservation != null)
          {
                response.StatusCode = 200;
                response.statusDescription = "Successfully";
                response.reservations = reservation;
          }
            return response;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetReservation(int id)
        {
            var response = new Response();

            response.StatusCode = 404;
            response.statusDescription = "Not Found";
            if (_context.Reservation == null)
          {
                response.StatusCode = 404;
                response.statusDescription = "Not Found";
          }
            var reservation = await _context.Reservation.FindAsync(id);

            if (reservation != null)
            {
                response.StatusCode = 200;
                response.statusDescription = "Successfully";
                response.reservations.Add(reservation);
            }

            return response;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutReservation(int id, Reservation reservation)
        {
            var response = new Response();
            response.StatusCode = 400;
            if (id != reservation.ReservationId)
            {
                response.StatusCode = 400;
                response.statusDescription = "Bad Request";
                return response;
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                response.StatusCode = 200;
                response.statusDescription = "updated";
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    response.StatusCode = 404;
                    response.statusDescription = "Not Found";
                    return response;
                }
                else
                {
                    throw;
                }
            }
  
        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostReservation(Reservation reservation)
        {
            var response = new Response();
            response.StatusCode = 201;
            response.statusDescription = "created";
            if (_context.Reservation == null)
          {
                response.StatusCode = 400;
                response.statusDescription = "ERROR";
                return response;
            }
            _context.Reservation.Add(reservation);
            response.reservations.Add(reservation);
            await _context.SaveChangesAsync();
            CreatedAtAction("GetReservation", new { id = reservation.ReservationId }, reservation);

            return response;
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteReservation(int id)
        {
            var response = new Response();
            response.StatusCode = 200;
            response.statusDescription = "Removed";
            if (_context.Reservation == null)
            {
                response.StatusCode = 404;
                response.statusDescription = "Not found";
                return response;
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                response.StatusCode = 404;
                response.statusDescription = "Not found";
                return response;
            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool ReservationExists(int id)
        {
            return (_context.Reservation?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }
    }
}
