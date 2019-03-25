using ASPNETCORE.Services.TeamService.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCORE.Repository
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext (DbContextOptions<TeamDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Team { get; set; }
    }
}
