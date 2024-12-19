using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPermission.Context;
using UserPermission.DTOs.User;
using UserPermission.Models;

namespace UserPermission.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UsersController(AppDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			List<User> users = await _context.Users.ToListAsync();
			return Ok(users);
		}
		[HttpPost]
		public async Task<IActionResult> Add(UserAddDto userAddDto)
		{
			UserPermission.Models.User user = new(userAddDto.Name, userAddDto.Surname, userAddDto.Email, userAddDto.Password);
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created);
		}
		[HttpPut]
		public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
		{
			var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userUpdateDto.Id);
			if (user != null)
			{
				user.Name = userUpdateDto.Name;
				user.Surname = userUpdateDto.Surname;
				user.Email = userUpdateDto.Email;
				user.Password = userUpdateDto.Password;
				user.UpdatedAt = DateTime.UtcNow;
				await _context.SaveChangesAsync();
				return Ok("User updated");
			}
			return BadRequest("User not found");
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
			if (user != null) 
				return Ok(user);
			return BadRequest("User not found");
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
			if (user == null) return BadRequest("User not found");
			 _context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return Ok("User deleted");

		}
	}
	}
