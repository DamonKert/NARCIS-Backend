using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Class;
using NarcisKH.Data;
using NarcisKH.Models;

namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly NarcisKHContext _context;

        public ClothesController(NarcisKHContext context)
        {
            _context = context;
        }

        // GET: api/Clothes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cloth>>> GetCloth()
        {
            List<ClothDTO> clothDTOs = new List<ClothDTO>();
            var cloths = await _context.Cloth.Include(x=>x.Category).Include(x=>x.Sizes).Include(x=>x.SizeAndClothQuantities).ToListAsync();
            foreach (var cloth in cloths)
            {
                ClothDTO clothDTO = new ClothDTO
                {
                    Id = cloth.Id,
                    Name = cloth.Name,
                    Description = cloth.Description,
                    Price = cloth.Price,
                    Category = cloth.Category,
                    ImagePaths = cloth.ImagePaths,
                    Discount = cloth.Discount
                };
                List<SizeAndQuantityDTO> sizeAndQuantityDTOs = new List<SizeAndQuantityDTO>();
                foreach (var sizeAndQuantity in cloth.Sizes)
                {
                    SizeAndQuantityDTO sizeAndQuantityDTO = new SizeAndQuantityDTO
                    {
                        Id = sizeAndQuantity.Id,
                        Name = sizeAndQuantity.Name,
                        Quantity = cloth.SizeAndClothQuantities.FirstOrDefault(x => x.SizeId == sizeAndQuantity.Id).Quantity
                    };
                    sizeAndQuantityDTOs.Add(sizeAndQuantityDTO);
                }
                clothDTO.Sizes = sizeAndQuantityDTOs;
                clothDTOs.Add(clothDTO);
            }
            return Ok(clothDTOs);
        }

        // GET: api/Clothes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cloth>> GetCloth(int id)
        {
            var cloth = await _context.Cloth.FindAsync(id);

            if (cloth == null)
            {
                return NotFound();
            }

            return cloth;
        }
        [HttpPost("GetCartItems")]
        public async Task<ActionResult> GetCartItems(List<int> clothIds)
        {
            List<ClothDTO> clothDTOs = new List<ClothDTO>();
            var cloths = await _context.Cloth.Include(x => x.Category).Include(x => x.Sizes).Include(x => x.SizeAndClothQuantities).Where(x=>clothIds.Contains(x.Id)).ToListAsync();
            foreach (var cloth in cloths)
            {
                ClothDTO clothDTO = new ClothDTO
                {
                    Id = cloth.Id,
                    Name = cloth.Name,
                    Description = cloth.Description,
                    Price = cloth.Price,
                    Category = cloth.Category,
                    ImagePaths = cloth.ImagePaths,
                    Discount = cloth.Discount
                };
                List<SizeAndQuantityDTO> sizeAndQuantityDTOs = new List<SizeAndQuantityDTO>();
                foreach (var sizeAndQuantity in cloth.Sizes)
                {
                    SizeAndQuantityDTO sizeAndQuantityDTO = new SizeAndQuantityDTO
                    {
                        Id = sizeAndQuantity.Id,
                        Name = sizeAndQuantity.Name,
                        Quantity = cloth.SizeAndClothQuantities.FirstOrDefault(x => x.SizeId == sizeAndQuantity.Id).Quantity
                    };
                    sizeAndQuantityDTOs.Add(sizeAndQuantityDTO);
                }
                clothDTO.Sizes = sizeAndQuantityDTOs;
                clothDTOs.Add(clothDTO);
            }
            return Ok(clothDTOs);
        }
        [HttpPost("GetClothByCategoryId")]
        public async Task<ActionResult> GetClothByCategoryId(int categoryId)
        {
            List<ClothDTO> clothDTOs = new List<ClothDTO>();
            var cloths = await _context.Cloth.Include(x => x.Category).Include(x => x.Sizes).Include(x => x.SizeAndClothQuantities).Where(x => x.CategoryId == categoryId).ToListAsync();
            foreach (var cloth in cloths)
            {
                ClothDTO clothDTO = new ClothDTO
                {
                    Id = cloth.Id,
                    Name = cloth.Name,
                    Description = cloth.Description,
                    Price = cloth.Price,
                    Category = cloth.Category,
                    ImagePaths = cloth.ImagePaths,
                    Discount = cloth.Discount
                };
                List<SizeAndQuantityDTO> sizeAndQuantityDTOs = new List<SizeAndQuantityDTO>();
                foreach (var sizeAndQuantity in cloth.Sizes)
                {
                    SizeAndQuantityDTO sizeAndQuantityDTO = new SizeAndQuantityDTO
                    {
                        Id = sizeAndQuantity.Id,
                        Name = sizeAndQuantity.Name,
                        Quantity = cloth.SizeAndClothQuantities.FirstOrDefault(x => x.SizeId == sizeAndQuantity.Id).Quantity
                    };
                    sizeAndQuantityDTOs.Add(sizeAndQuantityDTO);
                }
                clothDTO.Sizes = sizeAndQuantityDTOs;
                clothDTOs.Add(clothDTO);
            }
            return Ok(clothDTOs);
        }

        // PUT: api/Clothes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCloth(int id, Cloth cloth)
        {
            if (id != cloth.Id)
            {
                return BadRequest();
            }

            _context.Entry(cloth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClothExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clothes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cloth>> PostCloth(Cloth cloth)
        {
            _context.Cloth.Add(cloth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCloth", new { id = cloth.Id }, cloth);
        }

        // DELETE: api/Clothes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCloth(int id)
        {
            var cloth = await _context.Cloth.FindAsync(id);
            if (cloth == null)
            {
                return NotFound();
            }

            _context.Cloth.Remove(cloth);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("InitClothDate")]
        public async Task<ActionResult> InitClothDate()
        {
            var category = _context.Category.FirstOrDefault();
            if (category == null)
            {
                return BadRequest("Category not found");
            }
            var size = _context.Size.FirstOrDefault();
            if (size == null)
            {
                return BadRequest("Size not found");
            }
            var cloth = new Cloth
            {
                Name = "T-shirt",
                Description = "This is a T-shirt",
                Price = 10,
                CategoryId = category.Id,
                ImagePaths = new List<string> { "https://www.google.com" },
                Sizes = new List<Size> { size }
            };
            _context.Cloth.Add(cloth);
            await _context.SaveChangesAsync();
            var sizeAndClothQuantity = new SizeAndClothQuantity
            {
                ClothId = cloth.Id,
                SizeId = size.Id,
                Quantity = 10
            };
            _context.SizeAndClothQuantity.Add(sizeAndClothQuantity);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Init Cloth Data Success",
                Cloth = cloth
            };
            return Ok(successResponse);
        }

        private bool ClothExists(int id)
        {
            return _context.Cloth.Any(e => e.Id == id);
        }
    }
}
