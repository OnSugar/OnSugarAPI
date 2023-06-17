using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSugarAPI.Data;
using OnSugarAPI.Helpers;
using OnSugarAPI.Models;
using OnSugarAPI.ViewModels;

namespace OnSugarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BloodSugarController : ControllerBase
{
    private readonly OnSugarContext _context;
    private readonly IMapper _mapper;

    public BloodSugarController(OnSugarContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(BloodSugarViewModel viewModel)
    {
        var user = await UserHelper.GetUser(_context, User);

        var bloodSugarModel = _mapper.Map<BloodSugarModel>(viewModel);

        bloodSugarModel.UserModel = user;

        await _context.AddAsync(bloodSugarModel);
        await _context.SaveChangesAsync();

        return ResponseHelper.Success(true);
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(int offset = 0)
    {
        var user = await UserHelper.GetUser(_context, User);

        var result = await _context.BloodSugarModel
            .Skip(offset)
            .Take(100)
            .Select(m => new { Id = m.Id, Value = m.Value, Date = m.Date })
            .ToListAsync();

        return ResponseHelper.Success(result);
    }
}
