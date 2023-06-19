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
[Route("v1/[controller]")]
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

    [HttpPost("sync")]
    public async Task<IActionResult> Sync(List<BloodSugarViewModel> viewModels, DateTime lastSync)
    {
        var user = await UserHelper.GetUser(_context, User);

        var unsynced = await _context.BloodSugarModel.Where(m => m.UserModelId == user.Id && m.Date > lastSync).ToListAsync();
        var unsyncedViewModels = new List<BloodSugarViewModel>();
        foreach(var data in unsynced)
        {
            unsyncedViewModels.Add(_mapper.Map<BloodSugarViewModel>(data));
        }

        foreach(var viewModel in viewModels)
        {
            var model = _mapper.Map<BloodSugarModel>(viewModel);
            model.UserModel = user;

            await _context.AddAsync(model);
        }
        
        await _context.SaveChangesAsync();

        return ResponseHelper.Success(new Dictionary<string, object>
        {
            { "Add", unsyncedViewModels }
        });
    }
}
