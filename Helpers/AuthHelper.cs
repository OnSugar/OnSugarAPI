using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OnSugarAPI.Helpers
{
	public class AuthHelper
	{
		public const string ISSUER = "OnSugar";
		public const string AUDIENCE = "OnSugarApp";
		public static SymmetricSecurityKey GetKey => new (Encoding.UTF8.GetBytes("TESTKEY123456DASDASDASDADADASDA"));
	}
}

