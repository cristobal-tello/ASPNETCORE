using ASPNETCORE.Infrastructure.Notifications.Emitters.EventData;

namespace ASPNETCORE.Infrastructure.Notifications.Interfaces
{
    public interface INewTeamEventEmitter
    {
        void EmitNewTeamEvent(NewTeamEventData e);
    }
}