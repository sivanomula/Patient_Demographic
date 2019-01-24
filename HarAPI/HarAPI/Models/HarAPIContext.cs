using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarAPI.Models
{
    public class HarAPIContext : DbContext
        {
            public HarAPIContext(DbContextOptions<HarAPIContext> options)
                : base(options)
            {
            }

            public DbSet<Patient> Patients { get; set; }
        }
    
}
