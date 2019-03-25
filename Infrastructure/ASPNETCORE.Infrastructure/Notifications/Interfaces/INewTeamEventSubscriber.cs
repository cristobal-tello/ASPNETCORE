using ASPNETCORE.Infrastructure.Notifications.Emitters.EventData;

namespace ASPNETCORE.Infrastructure.Notifications.Interfaces
{
    public delegate void NewTeamEventDelegate(NewTeamEventData e);

    public interface INewTeamEventSubscriber
    {
        void Subscribe();
        void Unsubscribe();

        event NewTeamEventDelegate NewTeamEventReceived;
    }
}
