using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Class;
using NarcisKH.Data;
using NarcisKH.Models;
using NarcisKH.Models.S3Handler;

namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly NarcisKHContext _context;

        public ClothesController(NarcisKHContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Clothes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cloth>>> GetCloth()
        {
            List<ClothDTO> clothDTOs = new List<ClothDTO>();
            //var cloths = await _context.Clothes.Include(x=>x.Category).Include(x=>x.Sizes).Include(x=>x.SizeAndClothQuantities).ToListAsync();
			var cloths = await _context.Clothes.Include(x => x.Category).Include(x => x.Sizes).Include(x => x.Model).Include(x => x.SizeAndClothQuantities).ToListAsync();
			foreach (var cloth in cloths)
            {
                ClothDTO clothDTO = new ClothDTO
                {
                    Id = cloth.Id,
                    Name = cloth.Name,
                    Description = cloth.Description,
                    Price = cloth.Price,
                    Model = cloth.Model,
                    Category = cloth.Category,
                    ImagePaths = cloth.ImagePaths,
                    Discount = cloth.Discount,
                    Code = cloth.Code
                };
                List<SizeAndQuantityDTO> sizeAndQuantityDTOs = new List<SizeAndQuantityDTO>();
                //foreach (var sizeAndQuantity in cloth.Sizes)
                //{
                //    SizeAndQuantityDTO sizeAndQuantityDTO = new SizeAndQuantityDTO
                //    {
                //        Id = sizeAndQuantity.Id,
                //        Name = sizeAndQuantity.Name,
                //        Quantity = cloth.SizeAndClothQuantities.FirstOrDefault(x => x.SizeId == sizeAndQuantity.Id).Quantity
                //    };
                //    sizeAndQuantityDTOs.Add(sizeAndQuantityDTO);
                //}
                clothDTO.Sizes = cloth.Sizes;
                clothDTOs.Add(clothDTO);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Success",
                Data = clothDTOs
            };
            return Ok(successResponse);
        }

        // GET: api/Clothes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cloth>> GetCloth(int id)
        {
            var cloth = await _context.Clothes.FindAsync(id);

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
            var cloths = await _context.Clothes.Include(x => x.Category).Include(x => x.Sizes).Include(x => x.SizeAndClothQuantities).Where(x=>clothIds.Contains(x.Id)).ToListAsync();
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
                clothDTO.Size = sizeAndQuantityDTOs;
                clothDTOs.Add(clothDTO);
            }
            if(clothDTOs.Count == 0)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "Clothes not found"
                };
                return NotFound(notFoundResponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Success",
                Data = clothDTOs
            };
            return Ok(successResponse);
        }
        [HttpPost("GetClothByCategoryId")]
        public async Task<ActionResult> GetClothByCategoryId(GetClothByIdsRequest request)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
            if (category == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "Categories not found"
                };
                return NotFound(notFoundResponse);
            }
            else
            {
                var cloths = await _context.Clothes.Include(x => x.Category).Include(x => x.Sizes).Include(x => x.SizeAndClothQuantities).Where(x => x.CategoryId == request.CategoryId).ToListAsync();
                if (request.Sort != null)
                {
                    if (request.Sort.Mode == 1)
                    {
                        if (request.Sort.By == "ASC")
                        {
                            cloths = cloths.OrderBy(x => x.Price).ToList();
                        }
                        else
                        {
                            cloths = cloths.OrderByDescending(x => x.Price).ToList();
                        }
                    }else if (request.Sort.Mode == 2)
                    {
                        if (request.Sort.By == "ASC")
                        {
                            cloths = cloths.OrderBy(x => x.Name).ToList();
                        }
                        else
                        {
                            cloths = cloths.OrderByDescending(x => x.Name).ToList();
                        }
                    }else if(request.Sort.Mode == 3)
                    {
                        if (request.Sort.By == "ASC")
                        {
                            cloths = cloths.OrderBy(x => x.Discount).ToList();
                        }
                        else
                        {
                            cloths = cloths.OrderByDescending(x => x.Discount).ToList();
                        }
                    }else if(request.Sort.Mode == 4)
                    {
                        if (request.Sort.By == "ASC")
                        {
                            cloths = cloths.OrderBy(x => x.CreatedAt).ToList();
                        }
                        else
                        {
                            cloths = cloths.OrderByDescending(x => x.CreatedAt).ToList();
                        }
                    }else if(request.Sort.Mode == 5)
                    {
                        if (request.Sort.By == "ASC")
                        {
                            cloths = cloths.OrderBy(x => x.UpdatedAt).ToList();
                        }
                        else
                        {
                            cloths = cloths.OrderByDescending(x => x.UpdatedAt).ToList();
                        }
                    }

                }
                List<ClothDTO> clothDTOs = new List<ClothDTO>();
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
                    clothDTO.Size = sizeAndQuantityDTOs;
                    clothDTOs.Add(clothDTO);
                }
                if(clothDTOs.Count == 0)
                {
                    var notFoundResponse = new
                    {
                        StatusCode = 404,
                        Message = "Clothes not found"
                    };
                    return NotFound(notFoundResponse);
                }
                var successResponse = new
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = clothDTOs
                };
                return Ok(successResponse);

            }
        }

        // PUT: api/Clothes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("EditCloth/{id}")]
        public async Task<IActionResult> EditCloth([FromForm] UpdateClothRequest clothRequest)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == clothRequest.CategoryId);
            if (category == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "Category not found"
                };
                return NotFound(notFoundResponse);
            }
            var cloth = _context.Clothes.Include(x => x.Sizes).Include(x => x.SizeAndClothQuantities).FirstOrDefault(x => x.Id == clothRequest.ID);
            if (cloth == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "Cloth not found"
                };
                return NotFound(notFoundResponse);
            }
            if (clothRequest.RemainingImages.Count > 0)
            {
                cloth.ImagePaths.Clear();
                foreach (var image in clothRequest.RemainingImages)
                {
                    cloth.ImagePaths.Add(image);
                }
            }
            if (clothRequest.Images.Count > 0)
            {
                
                foreach (var image in clothRequest.Images)
                {
                    await using var ms = new MemoryStream();
                    image.CopyTo(ms);
                    var fileExt = image.FileName.Split('.').Last();
                    var fileName = Guid.NewGuid() + "." + fileExt;
                    if (fileExt != "jpg" && fileExt != "jpeg" && fileExt != "png")
                    {
                        var errorResponse = new
                        {
                            StatusCode = 400,
                            Message = "Invalid file type"
                        };
                        return BadRequest(errorResponse);
                    }
                    var s3object = new Models.S3Handler.S3Object
                    {
                        InputStream = ms,
                        Name = fileName,
                        BucketName = "cloth-images"
                    };
                    var AccessKey = _config["AwsConfiguration:AWSAccessKey"];
                    var SecretKey = _config["AwsConfiguration:AWSSecretKey"];
                    var awsCredentials = new AwsCredentials
                    {
                        AwsKey = _config["AwsConfiguration:AWSAccessKey"],
                        AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };
                    var imageUploadHelper = new ImageUploadHelper();
                    var response = await imageUploadHelper.UploadFileAsync(s3object, awsCredentials);
                    if (response.StatusCode != 200)
                    {
                        return BadRequest(response.Message);
                    }
                    else
                    {
                        cloth.ImagePaths.Add($"https://cloth-images.s3-ap-southeast-1.amazonaws.com/{fileName}");
                    }
                }
            }
            cloth.Name = clothRequest.Name;
            cloth.Description = clothRequest.Description;
            cloth.Price = clothRequest.Price;
            if(clothRequest.sizeAndQuantities.Count > 0)
            {
                foreach (var sizeAndQuantity in clothRequest.sizeAndQuantities)
                {
                    var size = _context.Sizes.FirstOrDefault(x => x.Id == sizeAndQuantity.Id);
                    if (size == null)
                    {
                        var notFoundResponse = new
                        {
                            StatusCode = 404,
                            Message = "Size not found"
                        };
                        return NotFound(notFoundResponse);
                    }
                    if (cloth.Sizes.FirstOrDefault(x => x.Id == size.Id) == null)
                    {
                        cloth.Sizes.Add(size);
                    }
                    var sizeAndClothQuantity = _context.SizeAndClothQuantities.FirstOrDefault(x => x.ClothId == cloth.Id && x.SizeId == size.Id);
                    if (sizeAndClothQuantity == null)
                    {
                        sizeAndClothQuantity = new SizeAndClothQuantity
                        {
                            ClothId = cloth.Id,
                            SizeId = size.Id,
                            Quantity = sizeAndQuantity.Quantity
                        };
                        _context.SizeAndClothQuantities.Add(sizeAndClothQuantity);
                    }
                    else
                    {
                        sizeAndClothQuantity.Quantity = sizeAndQuantity.Quantity;
                    }
                }
            }
            _context.Entry(cloth).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Cloth Updated",
                Data = cloth
            };
            return Ok(successResponse);
            
        }

        // POST: api/Clothes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cloth>> PostCloth([FromForm]CreateClothRequest cloth)
        {
            var model = _context.Models.FirstOrDefault(x => x.Id == cloth.ModelId);
            if (model == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "Model not found"
                };
                return NotFound(notFoundResponse);
            }

            var category = _context.Categories.FirstOrDefault(x => x.Id == cloth.CategoryId);
            if (category == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "Category not found"
                };
            }

            var newCloth = new Cloth
            {
                Name = cloth.Name,
                Description = cloth.Description,
                Price = cloth.Price,
                CategoryId = cloth.CategoryId,
                ImagePaths = new List<string>(),
                //Sizes = new List<Size>(),
                Model = model,
                Code = cloth.Code,
                Discount = cloth.Discount
            };
            
            if (cloth.SizeIDs.Count == 0)
            {
                var errorResponse = new
                {
                    StatusCode = 400,
                    Message = "Size not found"
                };
                return BadRequest(errorResponse);
            }
            foreach (var sizeId in cloth.SizeIDs)
            {
                var size = _context.Sizes.FirstOrDefault(x => x.Id == sizeId);
                if (size == null)
                {
                    var notFoundResponse = new
                    {
                        StatusCode = 404,
                        Message = "Size not found"
                    };
                    return NotFound(notFoundResponse);
                }
                newCloth.Sizes.Add(size);
            }
            if(cloth.Images != null)
            {
                if (cloth.Images.Count > 0)
                {
                    foreach (var image in cloth.Images)
                    {
                        await using var ms = new MemoryStream();
                        image.CopyTo(ms);
                        var fileExt = image.FileName.Split('.').Last();
                        var fileName = Guid.NewGuid() + "." + fileExt;
                        if (fileExt != "jpg" && fileExt != "jpeg" && fileExt != "png")
                        {
                            var errorResponse = new
                            {
                                StatusCode = 400,
                                Message = "Invalid file type"
                            };
                            return BadRequest(errorResponse);
                        }
                        var s3object = new Models.S3Handler.S3Object
                        {
                            InputStream = ms,
                            Name = fileName,
                            BucketName = "cloth-images"
                        };
                        var AccessKey = _config["AwsConfiguration:AWSAccessKey"];
                        var SecretKey = _config["AwsConfiguration:AWSSecretKey"];
                        var awsCredentials = new AwsCredentials
                        {
                            AwsKey = _config["AwsConfiguration:AWSAccessKey"],
                            AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
                        };
                        var imageUploadHelper = new ImageUploadHelper();
                        var response = await imageUploadHelper.UploadFileAsync(s3object, awsCredentials);
                        if (response.StatusCode != 200)
                        {
                            return BadRequest(response.Message);
                        }
                        else
                        {
                            newCloth.ImagePaths.Add($"https://cloth-images.s3-ap-southeast-1.amazonaws.com/{fileName}");
                        }
                    }
                }
            }
            

            //foreach (var sizeAndQuantity in cloth.sizeAndQuantities)
            //{
            //    var size = _context.Sizes.FirstOrDefault(x => x.Id == sizeAndQuantity.Id);
            //    if (size == null)
            //    {
            //        var notFoundResponse = new
            //        {
            //            StatusCode = 404,
            //            Message = "Size not found"
            //        };
            //        return NotFound(notFoundResponse);
            //    }
            //    newCloth.Sizes.Add(size);
            //    var sizeAndClothQuantity = new SizeAndClothQuantity
            //    {
            //        ClothId = newCloth.Id,
            //        SizeId = size.Id,
            //        Quantity = sizeAndQuantity.Quantity
            //    };
            //    _context.SizeAndClothQuantities.Add(sizeAndClothQuantity);
            //}
            _context.Clothes.Add(newCloth);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 201,
                Message = "Cloth Created",
                Data = newCloth
            };
            return Ok(successResponse);
        }

        // DELETE: api/Clothes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCloth(int id)
        {
            var cloth = await _context.Clothes.FindAsync(id);
            if (cloth == null)
            {
                return NotFound();
            }

            _context.Clothes.Remove(cloth);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("InitClothDate")]
        public async Task<ActionResult> InitClothDate()
        {
            var category = _context.Categories.FirstOrDefault();
            if (category == null)
            {
                return BadRequest("Categories not found");
            }
            var size = _context.Sizes.FirstOrDefault();
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
                Sizes = new List<Size> { size },
                Code = "TSHIRT"
            };
            _context.Clothes.Add(cloth);
            await _context.SaveChangesAsync();
            var sizeAndClothQuantity = new SizeAndClothQuantity
            {
                ClothId = cloth.Id,
                SizeId = size.Id,
                Quantity = 10
            };
            _context.SizeAndClothQuantities.Add(sizeAndClothQuantity);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Init Clothes Data Success",
                Cloth = cloth
            };
            return Ok(successResponse);
        }

        private bool ClothExists(int id)
        {
            return _context.Clothes.Any(e => e.Id == id);
        }
    }
}
