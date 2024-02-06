using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalProject.Model;

namespace FinalProject.Data
{
    public class FinalProjectContext : DbContext
    {
        public FinalProjectContext (DbContextOptions<FinalProjectContext> options)
            : base(options)
        {
        }

        public DbSet<FinalProject.Model.Product> Product { get; set; } = default!;
    }
}
