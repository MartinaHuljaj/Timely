using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timely.Models;

namespace Timely.DatabaseContext
{
    public class TimeContext :DbContext
    {
        public TimeContext(DbContextOptions<TimeContext> dbContextOptions)
  : base(dbContextOptions)
        {
        }


        public DbSet<Project> Projects { get; set; }
    }
}
