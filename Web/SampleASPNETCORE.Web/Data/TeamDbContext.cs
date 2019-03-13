using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASPNETCORE.Models;

namespace ASPNETCORE.Repository
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext (DbContextOptions<TeamDbContext> options)
            : base(options)
        {
        }

        public DbSet<ASPNETCORE.Models.Team> Team { get; set; }

        public DbSet<ASPNETCORE.Models.Member> Member { get; set; }
    }
}
