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

namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly NarcisKHContext _context;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;

        public OrdersController(NarcisKHContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var Orders = await _context.Orders.Include(x => x.Status)
                                     .ThenInclude(status => status.OrderStatus) 
                                 .Include(x => x.Status)
                                     .ThenInclude(status => status.PaymentStatus)
                                 .Include(x => x.Status)
                                     .ThenInclude(status => status.DeliveryStatus)
                                 .Include(x => x.Status)
                                     .ThenInclude(status => status.PaymentMethod) 
                                 .Include(x => x.CityProvince).Include(x => x.Employee).Include(x => x.clothes).ToListAsync();
            var response = new List<OrderDTO>();
            if (Orders.Count == 0)
            {
                var errorResponse = new
                {
                    StatusCode = 404,
                    Message = "Orders is not found",
                };
            }
            foreach (var order in Orders)
            {
                var orderDTO = new OrderDTO
                {
                    Id = order.Id,
                    FullName = order.FullName,
                    Address = order.Address,
                    Phone = order.Phone,
                    Note = order.Note,
                    CityProvince = order.CityProvince,
                    Status = new StatusDTO
                    {
                        OrderStatus = order.Status.OrderStatus,
                        PaymentStatus = order.Status.PaymentStatus,
                        DeliveryStatus = order.Status.DeliveryStatus,
                        PaymentMethod = order.Status.PaymentMethod,
                        TransactionProofImage = order.Status.TransactionProofImage,
                        DeliveryProofImage = order.Status.DeliveryProofImage
                    },
                    Employee = order.Employee,
                    CreatedDate = order.CreatedDate,
                    UpdatedDate = order.UpdatedDate,
                    clothes = new List<ClothDTO>()
                };
                foreach (var cloth in order.clothes)
                {
                    var clothDTO = new ClothDTO
                    {
                        Id = cloth.Cloth.Id,
                        Name = cloth.Cloth.Name,
                        Description = cloth.Cloth.Description,
                        Price = cloth.Cloth.Price,
                        Discount = cloth.Cloth.Discount,
                        Sizes = cloth.Cloth.Sizes,
                        Size = new List<SizeAndQuantityDTO>()
                    };
                    foreach (var size in cloth.Cloth.SizeAndClothQuantities)
                    {
                        var sizeAndQuantityDTO = new SizeAndQuantityDTO
                        {
                            Id = size.Size.Id,
                            Name = size.Size.Name,
                            Quantity = size.Quantity
                        };
                        clothDTO.Size.Add(sizeAndQuantityDTO);
                    }
                    orderDTO.clothes.Add(clothDTO);
                }
                response.Add(orderDTO);
            }
            
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Orders is found",
                Data = response
            };
            return Ok(successResponse);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromForm]CreateOrderRequest createOrder)
        {
            if(createOrder.ClothSizeQuantities.Count == 0)
            {
                var errorResponse = new
                {
                    StatusCode = 400,
                    Message = "Clothes is required",
                };
                return BadRequest(errorResponse);
            }
            var DeliveryStatus = await _context.DeliveryStatuses.FindAsync(createOrder.DeliveryStatusId);
            if (DeliveryStatus == null)
            {
                   var errorResponse = new
                   {
                       StatusCode = 404,
                       Message = "DeliveryStatusId is not found",
                   };
            }
            var OrderStatus = await _context.OrderStatuses.FindAsync(createOrder.OrderStatusId);
            if (OrderStatus == null)
            {
                   var errorResponse = new
                   {
                       StatusCode = 404,
                       Message = "OrderStatusId is not found",
                   };
            }
            var PaymentMethod = await _context.PaymentMethods.FindAsync(createOrder.PaymentMethodId);
            if (PaymentMethod == null)
            {
                   var errorResponse = new
                   {
                       StatusCode = 404,
                       Message = "PaymentMethodId is not found",
                   };
            }
            var User = await _context.Users.FindAsync(createOrder.EmployeeId);
            if (User == null)
            {
                   var errorResponse = new
                   {
                       StatusCode = 404,
                       Message = "EmployeeId is not found",
                   };
            }
            var CityProvince = await _context.CityProvinces.FindAsync(createOrder.CityProvinceId);
            if (CityProvince == null)
            {
                  var errorResponse = new
                  {
                      StatusCode = 404,
                      Message = "CityProvinceId is not found",
                  };
            }
            var PaymentStatus = await _context.PaymentStatuses.FindAsync(createOrder.PaymentStatusId);
            if (PaymentStatus == null)
            {
                   var errorResponse = new
                   {
                       StatusCode = 404,
                       Message = "PaymentStatusId is not found",
                   };
            }
            Status status = new Status
            {
                OrderStatus = OrderStatus,
                DeliveryStatus = DeliveryStatus,
                PaymentMethod = PaymentMethod,
                PaymentStatus = PaymentStatus
            };
           

            var order = new Order
            {
                FullName = createOrder.FullName,
                Address = createOrder.Address,
                Phone = createOrder.PhoneNumber,
                Note = createOrder.Note,
                CityProvince = CityProvince,
                Status = status,
                Employee = User,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            if(createOrder != null)
            {
                foreach (var clothObject in createOrder.ClothSizeQuantities)
                {
                   var cloth = await _context.Clothes.FindAsync(clothObject.ClothId);
                    if (cloth == null)
                    {
                          var errorResponse = new
                          {
                            StatusCode = 404,
                            Message = "ClothId is not found",
                          };
                        return NotFound(errorResponse);
                     }
                  var clothSizeQuantity = new ClothSizeQuantity
                  {
                      Cloth = cloth,
                      Size = clothObject.Size,
                      Quantity = clothObject.Quantity
                  };
                    order.clothes.Add(clothSizeQuantity);

                }
            }
            if (createOrder.DeliveryProofImage != null)
            {
                await using var ms = new MemoryStream();
                createOrder.DeliveryProofImage.CopyTo(ms);
                var fileExtension = Path.GetExtension(createOrder.DeliveryProofImage.FileName);
                var fileName = Guid.NewGuid() + fileExtension;
                if (fileExtension != ".jpg" && fileExtension != ".png" && fileExtension != ".jpeg")
                {
                    var errorResponse = new
                    {
                        StatusCode = 404,
                        Message = "DeliveryProofImage is not valid",
                    };
                }
                var so3object = new S3Object()
                {
                    InputStream = ms,
                    Name = fileName,
                    BucketName = "images-proof"
                };
                var AccessKey = _config["AwsConfiguration:AWSAccessKey"];
                var SecretKey = _config["AwsConfiguration:AWSSecretKey"];
                var credentials = new AwsCredentials
                {
                    AwsKey = AccessKey,
                    AwsSecretKey = SecretKey
                };
                var imageUploadHelper = new ImageUploadHelper();
                var result = await imageUploadHelper.UploadFileAsync(so3object, credentials);
                if (result.StatusCode != 200)
                {
                    var errorResponse = new
                    {
                        StatusCode = 400,
                        Message = "DeliveryProofImage is not valid",
                    };
                }
                else
                {
                    status.DeliveryProofImage.Add($"https://images-proof.s3.ap-southeast-1.amazonaws.com/images-proof/{fileName}");
                }
            }
            if (createOrder.PaymentProofImage != null)
            {
                await using var ms = new MemoryStream();
                createOrder.PaymentProofImage.CopyTo(ms);
                var fileExtension = Path.GetExtension(createOrder.PaymentProofImage.FileName);
                var fileName = Guid.NewGuid() + fileExtension;
                if (fileExtension != ".jpg" && fileExtension != ".png" && fileExtension != ".jpeg")
                {
                    var errorResponse = new
                    {
                        StatusCode = 400,
                        Message = "PaymentProofImage is not valid",
                    };
                }
                var so3object = new S3Object()
                {
                    InputStream = ms,
                    Name = fileName,
                    BucketName = "images-proof"
                };
                var AccessKey = _config["AwsConfiguration:AWSAccessKey"];
                var SecretKey = _config["AwsConfiguration:AWSSecretKey"];
                var credentials = new AwsCredentials
                {
                    AwsKey = AccessKey,
                    AwsSecretKey = SecretKey

                };
                var imageUploadHelper = new ImageUploadHelper();
                var result = await imageUploadHelper.UploadFileAsync(so3object, credentials);
                if (result.StatusCode != 200)
                {
                    var errorResponse = new
                    {
                        StatusCode = 400,
                        Message = "PaymentProofImage is not valid",
                    };
                }
                else
                {
                    status.TransactionProofImage = ($"https://images-proof.s3.ap-southeast-1.amazonaws.com/images-proof/{fileName}");
                }
            }
            _context.Statuses.Add(status);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var successResponse = new 
            {
                StatusCode = 200,
                Message = "Order is created successfully",
                Data = order
            };
            return Ok(successResponse);

        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("InitializeOrderData")]
        public async Task<ActionResult<Order>> InitializeOrderData()
        {
            var DeliveryStatus = await _context.DeliveryStatuses.FirstOrDefaultAsync();
            var OrderStatus = await _context.OrderStatuses.FirstOrDefaultAsync();
            var PaymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync();
            var User = await _context.Users.FirstOrDefaultAsync();
            var CityProvince = await _context.CityProvinces.FirstOrDefaultAsync();
            Status status = new Status
            {
                OrderStatus = OrderStatus,
                DeliveryStatus = DeliveryStatus,
                PaymentMethod = PaymentMethod
            };
            var order = new Order
            {
                FullName = "TengSambo",
                Address = "PhnomPenh",
                Phone = "0751234567",
                Note = "Note",
                CityProvince = CityProvince,
                Status = status,
                Employee = User,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _context.Statuses.Add(status);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
