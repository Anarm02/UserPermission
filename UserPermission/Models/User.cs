using System.ComponentModel.DataAnnotations;

namespace UserPermission.Models
{
	public class User:BaseEntity
	{
		public User()
		{
			
		}
		public User(string name,string surname,string email,string password)
		{
			Name= name;
			Surname= surname;
			Email= email;
			Password= password;
		}

		public string Name { get; set; }
		public string Surname { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
