using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OnSugarAPI.Models
{
	public class BloodSugarModel
	{
		public int Id { get; set; }

		[Precision(1)]
		public float Value { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

		public int UserModelId { get; set; }
		public UserModel UserModel { get; set; } = default!;
	}
}
