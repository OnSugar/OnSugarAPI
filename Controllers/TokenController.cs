using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using OnSugarAPI.Helpers;

namespace OnSugarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    [HttpPost("get")]
    public IActionResult Get(string username)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

        var jwt = new JwtSecurityToken(
            issuer: AuthHelper.ISSUER,
            audience: AuthHelper.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(AuthHelper.GetKey, SecurityAlgorithms.HmacSha256)
        );

        return new JsonResult(new Dictionary<string, string>
        {
            { "Token", new JwtSecurityTokenHandler().WriteToken(jwt) }
        });
    }

    [Authorize]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return new JsonResult(new Dictionary<string, string>
        {
            { "Data", User.Identity!.Name! }
        });
    }
}
