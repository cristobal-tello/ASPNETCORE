using ASPNETCORE.Services.TeamService.Models;
using System.Threading.Tasks;

namespace ASPNETCORE.Services.Clients.Interfaces

{
    public interface IHttpNotificationTeamServiceClient
    {
        Task NewTeamAsync(Team team);
    }
}
