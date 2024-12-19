using System.ComponentModel.DataAnnotations;

namespace UserPermission.DTOs.User
{
	public class UserUpdateDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
