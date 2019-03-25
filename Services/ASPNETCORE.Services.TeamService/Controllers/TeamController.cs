using ASPNETCORE.Repository;
using ASPNETCORE.Services.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCORE.Services.TeamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamDbContext teamDbContext;

        public TeamController(TeamDbContext context)
        {
            teamDbContext = context;
        }

        // GET: api/Team
        [HttpGet]
        public IEnumerable<Team> GetTeams()
        {
            IEnumerable<Team> teams = new List<Team>()
            {
                new Team()
                {
                    ID = Guid.NewGuid(),
                    Name = "Equipo A"
                },
                new Team()
                {
                    ID = Guid.NewGuid(),
                    Name = "Equipo B"
                }
            };

            //return _context.Team;
            return teams;
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await teamDbContext.Team.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam([FromRoute] Guid id, [FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.ID)
            {
                return BadRequest();
            }

            teamDbContext.Entry(team).State = EntityState.Modified;

            try
            {
                await teamDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Team
        [HttpPost]
        public async Task<IActionResult> PostTeam([FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            teamDbContext.Team.Add(team);
            await teamDbContext.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.ID }, team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await teamDbContext.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            teamDbContext.Team.Remove(team);
            await teamDbContext.SaveChangesAsync();

            return Ok(team);
        }

        private bool TeamExists(Guid id)
        {
            return teamDbContext.Team.Any(e => e.ID == id);
        }
    }
}