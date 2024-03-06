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
using NarcisKH.Models.S3Handler;
using NuGet.Versioning;

namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly NarcisKHContext _context;
        private readonly IConfiguration _config;

        public ModelsController(NarcisKHContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Models
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        {
            var models = await _context.Models.Include(x=>x.Clothes).ToListAsync();
            if(models.Count == 0)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "No Models Found"
                };
                return NotFound(notFoundResponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Sucess",
                Data = models
            };
            return Ok(successResponse);
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(int id)
        {
            var model = await _context.Models.Include(x=>x.Clothes).FirstOrDefaultAsync(x=>x.Id == id);

            if (model == null)
            {
               var notFoundResponse = new
               {
                   StatusCode = 404,
                   Message = "Model Not Found"
               };
                return NotFound(notFoundResponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Sucess",
                Data = model
            };

            return Ok(successResponse);
        }

        // PUT: api/Models/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("EditModel")]
        public async Task<IActionResult> PutModel([FromForm] UpdateModelRequest model)
        {
            if(model.ID == null)
            {
                var badRequestResponse = new
                {
                    StatusCode = 400,
                    Message = "Model Id is required"
                };
                return BadRequest(badRequestResponse);
            }

            var modelToUpdate = await _context.Models.FirstOrDefaultAsync(x=>x.Id == model.ID);
            if(modelToUpdate == null)
            {
                var notFoundResponse = new
                {
                    StatusCode = 404,
                    Message = "Model Not Found"
                };
                return NotFound(notFoundResponse);
            }
            if(model.Profile != null)
            {
                if (modelToUpdate.ProfilePicture != null)
                {
                    var oldFileName = modelToUpdate.ProfilePicture.Split("/").Last();
                    var credentials = new AwsCredentials
                    {
                        AwsKey = _config["AwsConfiguration:AWSAccess"],
                        AwsSecretKey = _config["AwsConfiguration:AWSSecret"]
                    };

                    var deleteResponse = await new ImageUploadHelper().DeleteFileAsync(oldFileName, "narciskh-model-images", credentials);
                    if (deleteResponse.StatusCode != 200)
                    {
                        return BadRequest(deleteResponse.Message);
                    }
                }
                    await using var memoryStream = new MemoryStream();
                    await model.Profile.CopyToAsync(memoryStream);
                    var fileExtension = model.Profile.FileName.Split(".")[1];
                    if (fileExtension != "jpg" && fileExtension != "jpeg" && fileExtension != "png")
                    {
                        return BadRequest("Invalid file type");
                    }
                    var fileName = $"{Guid.NewGuid()}.{fileExtension}";

                    var s3Object = new S3Object
                    {
                        InputStream = memoryStream,
                        Name = fileName,
                        BucketName = "narciskh-model-images",

                    };
                    var AccessKey = _config["AwsConfiguration:AWSAccessKey"];
                    var SecretKey = _config["AwsConfiguration:AWSSecretKey"];
                    var awsCredentials = new AwsCredentials
                    {
                        AwsKey = _config["AwsConfiguration:AWSAccess"],
                        AwsSecretKey = _config["AwsConfiguration:AWSSecret"]
                    };
                    var imageUploadHelper = new ImageUploadHelper();
                    var response = await imageUploadHelper.UploadFileAsync(s3Object, awsCredentials);
                    if (response.StatusCode != 200)
                    {
                        return BadRequest(response.Message);
                    }
                    else
                    {
                        modelToUpdate.ProfilePicture = $"https://narciskh-model-images.s3-ap-southeast-1.amazonaws.com/{fileName}";
                    }
                  
                
            }
            modelToUpdate.Name = model.Name;
            modelToUpdate.Age = model.Age;
            modelToUpdate.Height = model.Height;
            modelToUpdate.Weight = model.Weight;
            modelToUpdate.Bottom = model.Bottom;
            modelToUpdate.Top = model.Top;
            _context.Entry(modelToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Model Updated Successfully",
                Data = modelToUpdate
            };
            return Ok(successResponse);
        }

        // POST: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostModel([FromForm]CreateModelRequest model)
        {
            string imagePath = "";

            //if (ValidateHelper.ValidateObject(model).Count > 0)
            //{
            //    return BadRequest(ValidateHelper.ValidateObject(model));
            //}
            //Check if this request has a file
            if (model.Profile == null)
            {
                return BadRequest("Image is required");
            }
            if(Request.Form.Files.Count > 0)
            {
                await using var memoryStream = new MemoryStream();
                await model.Profile.CopyToAsync(memoryStream);
               var fileExtension = model.Profile.FileName.Split(".")[1];
                if(fileExtension != "jpg" && fileExtension != "jpeg" && fileExtension != "png")
                {
                    return BadRequest("Invalid file type");
                }
                var fileName =$"{Guid.NewGuid()}.{fileExtension}";

                var s3Object = new S3Object
                {
                    InputStream = memoryStream,
                    Name = fileName,
                    BucketName = "narciskh-model-images",
                    
                };
                var AccessKey = _config["AwsConfiguration:AWSAccessKey"];
                var SecretKey = _config["AwsConfiguration:AWSSecretKey"];
                Console.WriteLine(AccessKey);
                Console.WriteLine(SecretKey);
                var awsCredentials = new AwsCredentials
                {
                    AwsKey = _config["AwsConfiguration:AWSAccessKey"],
                    AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
                };
                var imageUploadHelper = new ImageUploadHelper();
                var response = await imageUploadHelper.UploadFileAsync(s3Object, awsCredentials);
                if(response.StatusCode != 200)
                {
                    return BadRequest(response.Message);
                }
                else
                {
                    imagePath = $"https://narciskh-model-images.s3-ap-southeast-1.amazonaws.com/{fileName}";
                }
            }
            var createModel = new Model
            {
                Name = model.Name,
                Age = model.Age,
                Height = model.Height,
                Weight = model.Weight,
                Bottom = model.Bottom,
                Top = model.Top,
                ProfilePicture = imagePath
            };
            _context.Models.Add(createModel);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 201,
                Message = "Model Created Successfully",
                Data = createModel
            };
           return CreatedAtAction("GetModel", new { id = createModel.Id }, successResponse);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
               var notFoundResponse = new
               {
                   StatusCode = 404,
                   Message = "Model Not Found"
               };
                return NotFound(notFoundResponse);
            }
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Model Deleted Successfully"
            };
            return Ok(successResponse);
        }

        private bool ModelExists(int id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
