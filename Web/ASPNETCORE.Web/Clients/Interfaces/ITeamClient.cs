using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNETCORE.Web.Models;

namespace ASPNETCORE.Web.Client.Interfaces
{
    public interface ITeamClient
    {
        Task<Team> GetTeam(Guid teamID);
        Task<ICollection<Team>> GetTeamsAsync();

        Task AddTeamAsync(Team team);
    }
}
