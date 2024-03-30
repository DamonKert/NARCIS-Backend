using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Class.Customer.Cart;
using NarcisKH.Class.Customer.DTO;
using NarcisKH.Data;
using NarcisKH.Models;

namespace NarcisKH.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly NarcisKHContext _context;
		private readonly Microsoft.Extensions.Configuration.IConfiguration _config;

		public CustomerController(NarcisKHContext context, IConfiguration config)
		{
			_context = context;
			_config = config;
		}

		[HttpPost("GetParentCategory")]
		public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
		{
			var categories = await _context.Categories.Include(x => x.Children).Include(x => x.Parent).Where(x => x.ParentId == null).ToListAsync();

			List<CategoryDTO> categoryDTO = new List<CategoryDTO>();
			foreach (var category in categories)
			{
				CategoryDTO temp = new CategoryDTO()
				{
					Id = category.Id,
					Name = category.Name,
				};
				foreach (var child in category.Children)
				{
					var childTemp = new ChildDTO()
					{
						Id = child.Id,
						Name = child.Name,
					};
					temp.Childs.Add(childTemp);
				}
				categoryDTO.Add(temp);
			}
			var successResponse = new
			{
				StatusCode = 200,
				Message = "Success",
				Data = categoryDTO
			};
			return Ok(successResponse);
		}

		[HttpGet("GetCategoryByID/{id}")]
		public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory(int id)
		{
			var category = _context.Categories.Include(x => x.Children).Include(x => x.Parent).FirstOrDefault(x => x.Id == id);

			if (category == null)
			{
				var NotFoundResponse = new
				{
					StatusCode = 404,
					Message = "Category not found"
				};
				return NotFound(NotFoundResponse);
			}
			CategoryDTO categoryDTO = new CategoryDTO()
			{
				Id = category.Id,
				Name = category.Name,
			};
			foreach (var child in category.Children)
			{
				var childTemp = new ChildDTO()
				{
					Id = child.Id,
					Name = child.Name,
				};
				categoryDTO.Childs.Add(childTemp);
			}
			var successResponse = new
			{
				StatusCode = 200,
				Message = "Success",
				Data = categoryDTO
			};
			return Ok(successResponse);
		}


		[HttpPost("GetListClothCart")]
		public async Task<ActionResult<IEnumerable<ClothDTO>>> GetClothInCart(GetCartRequest request)
		{
			var clothes = await _context.Clothes
		.Include(x => x.Category)
		.Include(x => x.Sizes)
		.Include(x => x.Model)
		.Where(x => request.Clothes.Select(item => item.ClothId).Contains(x.Id))
		.ToListAsync();
			GetCartResponse response = new GetCartResponse();

			foreach (var cloth in request.Clothes)
			{
				var Check = clothes.FirstOrDefault(c => c.Id == cloth.ClothId && c.Sizes != null && c.Sizes.Any(s => s.Id == cloth.SizeId));
				if (Check != null && Check.Sizes != null)
				{
					ClothDTO Temp = new ClothDTO
					{
						Category = Check.Category,
						Id = Check.Id,
						Code = Check.Code,
						Description = Check.Description,
						ImagePaths = Check.ImagePaths,
						Sizes = Check.Sizes,
						Discount = Check.Discount,
						Model = Check.Model,
						Name = Check.Name,
						Price = Check.Price,
						Detail = new Detail
						{
							Size = Check.Sizes.First(s => s.Id == cloth.SizeId),
							Quantity = cloth.Quantity
						}
					};
					response.Clothes.Add(Temp);
				}
				else
				{
					var BadRequestResponse = new
					{
						StatusCode = 400,
						Message = "ClothId " + cloth.ClothId + " does not contain SizeId " + cloth.SizeId,
					};
					return BadRequest(BadRequestResponse);
				}
			}


			var successResponse = new
			{
				StatusCode = 200,
				Message = "Orders is found",
				Data = response.Clothes
			};
			return Ok(successResponse);

		}
	}
}
