using Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set;  } 
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<OurVisionComponent> OurVisionComponents { get; set; }
        public DbSet<OurVision> OurVisions { get; set; }

    }
}
