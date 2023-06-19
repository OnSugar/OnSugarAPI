using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnSugarAPI.Helpers;
using OnSugarAPI.Data;
using Microsoft.AspNetCore.Identity;
using OnSugarAPI.Models;

namespace OnSugarAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class TokenController : ControllerBase
{
    private readonly OnSugarContext _context;
    private readonly PasswordHasher<UserModel> _hasher = new();

    public TokenController(OnSugarContext context)
    {
        _context = context;
    }

    [HttpPost("get")]
    public async Task<IActionResult> Get(string email, string password)
    {
        var user = await _context.UserModel.FirstOrDefaultAsync(m => m.Email == email);
        if(user == null)
        {
            return ResponseHelper.Error(0, "User not found");
        }

        if(_hasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Success)
        {
            return ResponseHelper.Error(0, "User not found");
        }

        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var jwt = new JwtSecurityToken(
            issuer: AuthHelper.ISSUER,
            audience: AuthHelper.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(AuthHelper.GetKey, SecurityAlgorithms.HmacSha256)
        );

        return ResponseHelper.Success(new Dictionary<string, string>
        {
            { "Token", new JwtSecurityTokenHandler().WriteToken(jwt) }
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([Bind("Id,Email,FirstName,LastName,Password,Date")] UserModel userModel)
    {
        var existingUser = await _context.UserModel.FirstOrDefaultAsync(m => m.Email == userModel.Email);

        if(existingUser != null)
        {
            return ResponseHelper.Error(0, "User with this email already exists");
        }

        userModel.Password = _hasher.HashPassword(userModel, userModel.Password);

        await _context.AddAsync(userModel);
        await _context.SaveChangesAsync();

        return ResponseHelper.Success(new Dictionary<string, object>
        {
            { "Id", userModel.Id }
        });
    }
}
