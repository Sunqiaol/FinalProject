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
    public class RestaurantsController : ControllerBase
    {
        private readonly FinalProjectDBContext _context;

        public RestaurantsController(FinalProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<Response>> GetRestaurants()
        {
            var response = new Response();
            var restaurant = await _context.Restaurant.ToListAsync();
            response.StatusCode = 404;
            response.statusDescription = "Not Found";
            if (_context.Restaurant != null)
          {
                response.StatusCode = 200;
                response.statusDescription = "Successfully";
                response.restaurants = restaurant;
            }
            return response;
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetRestaurant(int id)
        {
            var response = new Response();

            response.StatusCode = 404;
            response.statusDescription = "Not Found";
            if (_context.Restaurant == null)
          {
                response.StatusCode = 404;
                response.statusDescription = "Not Found";
                return response;
            }
            var restaurant = await _context.Restaurant.FindAsync(id);

            if (restaurant != null)
            {
                response.StatusCode = 200;
                response.statusDescription = "Successfully";
                response.restaurants.Add(restaurant);
            }

            return response;
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutRestaurant(int id, Restaurant restaurant)
        {
            var response = new Response();
            response.StatusCode = 400;
            if (id != restaurant.RestaurantID)
            {
                response.StatusCode = 400;
                response.statusDescription = "Bad Request";
                return response;
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                response.StatusCode = 200;
                response.statusDescription = "updated";
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostRestaurant(Restaurant restaurant)
        {
            var response = new Response();
            response.StatusCode = 201;
            response.statusDescription = "created";
            if (_context.Restaurant == null)
          {
                response.StatusCode = 400;
                response.statusDescription = "ERROR";
                return response;
            }
            _context.Restaurant.Add(restaurant);
            response.restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            CreatedAtAction("GetRestaurant", new { id = restaurant.RestaurantID }, restaurant);
            return response;
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteRestaurant(int id)
        {
            var response = new Response();
            response.StatusCode = 200;
            response.statusDescription = "Removed";
            if (_context.Restaurant == null)
            {
                response.StatusCode = 404;
                response.statusDescription = "Not found";
                return response;
            }
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                response.StatusCode = 404;
                response.statusDescription = "Not found";
                return response;
            }

            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();

            return response;
        }

        private bool RestaurantExists(int id)
        {
            return (_context.Restaurant?.Any(e => e.RestaurantID == id)).GetValueOrDefault();
        }
    }
}
