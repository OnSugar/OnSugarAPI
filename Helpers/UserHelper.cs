using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OnSugarAPI.Data;
using OnSugarAPI.Models;

namespace OnSugarAPI.Helpers
{
	public class UserHelper
	{
		public async static Task<UserModel> GetUser(OnSugarContext context, ClaimsPrincipal user)
		{
			var id = int.Parse(user.Claims.First(c => c.Type == "Id").Value);

			var model = await context.UserModel.FirstOrDefaultAsync(m => m.Id == id);

			return model!;
		}
	}
}

