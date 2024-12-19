using Microsoft.EntityFrameworkCore;
using UserPermission.Models;

namespace UserPermission.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{
			
		}
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Permission> Permissions { get; set; }
	}
}
