using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPermission.Context;
using UserPermission.DTOs.Permission;
using UserPermission.DTOs.User;
using UserPermission.Models;

namespace UserPermission.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PermissionsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public PermissionsController(AppDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			List<Permission> permissions = await _context.Permissions.ToListAsync();
			return Ok(permissions);
		}
		[HttpPost]
		public async Task<IActionResult> Add(PermissionAddDto permissionAddDto)
		{
			Permission permission = new(permissionAddDto.Name, permissionAddDto.Description);
			await _context.Permissions.AddAsync(permission);
			await _context.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created);
		}
		[HttpPut]
		public async Task<IActionResult> Update(PermissionUpdateDto permissionUpdateDto)
		{
			var permission = await _context.Permissions.FirstOrDefaultAsync(permission => permission.Id == permissionUpdateDto.Id);
			if (permission != null)
			{
				permission.Name = permissionUpdateDto.Name;
				permission.Description = permissionUpdateDto.Description;
				permission.UpdatedAt = DateTime.UtcNow;
				await _context.SaveChangesAsync();
				return Ok("Permission updated");
			}
			return BadRequest("Permission not found");
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var permission = await _context.Permissions.FirstOrDefaultAsync(permission => permission.Id == id);
			if (permission != null)
				return Ok(permission);
			return BadRequest("Permission not found");
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var permission = await _context.Permissions.FirstOrDefaultAsync(permission => permission.Id == id);
			if (permission == null) return BadRequest("Permission not found");
			_context.Permissions.Remove(permission);
			await _context.SaveChangesAsync();
			return Ok("Permission deleted");

		}
	}
}
