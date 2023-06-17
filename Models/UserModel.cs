using System;
using System.ComponentModel.DataAnnotations;

namespace OnSugarAPI.Models
{
	public class UserModel
	{
		public int Id { get; set; }

		public string Email { get; set; } = default!;

		public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

		public string Password { get; set; } = default!;

		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; } = DateTime.Now;

		public List<BloodSugarModel> BloodSugars { get; set; } = new();
	}
}
