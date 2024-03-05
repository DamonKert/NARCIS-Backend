using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Data;
using NarcisKH.Models;

namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityProvincesController : ControllerBase
    {
        private readonly NarcisKHContext _context;

        public CityProvincesController(NarcisKHContext context)
        {
            _context = context;
        }

        // GET: api/CityProvinces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityProvince>>> GetCityProvinces()
        {
            var cityProvinces = await _context.CityProvinces.ToListAsync();
            if(cityProvinces.Count == 0)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "CityProvinces Not Found"
                };
                return NotFound(notFoundResponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Success",
                Data = cityProvinces
            };
            return Ok(successResponse);
        }

        // GET: api/CityProvinces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityProvince>> GetCityProvince(int id)
        {
            var cityProvince = await _context.CityProvinces.FindAsync(id);

            if (cityProvince == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "CityProvince id: "+id +" Not Found"
                };
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Success",
                Data = cityProvince
            };

            return cityProvince;
        }

        [HttpPost("Edit/{id}")]
        public async Task<ActionResult<CityProvince>> EditCityProvince(int id, float cost)
        {
            var cityProvince = await _context.CityProvinces.FindAsync(id);
            if (cityProvince == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "CityProvince Not Found"
                };
            }
            cityProvince.DeliveryFee = cost;

            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Updated",
                Data = cityProvince
            };
            return Ok(successResponse);

        }
        // POST: api/CityProvinces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityProvince>> PostCityProvince(CityProvince cityProvince)
        {
            _context.CityProvinces.Add(cityProvince);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCityProvince", new { id = cityProvince.Id }, cityProvince);
        }


        private bool CityProvinceExists(int id)
        {
            return _context.CityProvinces.Any(e => e.Id == id);
        }
    }
}
