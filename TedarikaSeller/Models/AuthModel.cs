using System.ComponentModel.DataAnnotations;

namespace TedarikaSeller.Models
{
	public class AuthModel
	{
	}
	public class AuthRegisterModel
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
		public string Password { get; set; }
	}

	public class AuthLoginModel
	{
		[Required(ErrorMessage = "User identifier (email or phone) is required")]
		public string UserIdentifier { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }
	}

}
