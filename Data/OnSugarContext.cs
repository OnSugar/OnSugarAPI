using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnSugarAPI.Models;

namespace OnSugarAPI.Data
{
    public class OnSugarContext : DbContext
    {
        public OnSugarContext (DbContextOptions<OnSugarContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> UserModel { get; set; } = default!;
        public DbSet<BloodSugarModel> BloodSugarModel { get; set; } = default!;
    }
}
