using ASPNETCORE.Infrastructure.Notifications.Emitters.EventData;
using ASPNETCORE.Infrastructure.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ASPNETCORE.Services.NotificationTeamService.Controllers
{
    [Route("/services/{memberId}/notificationteam")]
    public class NotificationTeamController : Controller
    {
        private readonly INewTeamEventEmitter newTeamEventEmitter;

        public NotificationTeamController(INewTeamEventEmitter eventEmitter)
        {
            this.newTeamEventEmitter = eventEmitter;
        }

        [HttpPost]
        public IActionResult NewMember(Guid memberId)
        {
            NewTeamEventData newMemberEvent = new NewTeamEventData()
            {
                Name = string.Format("{0} at {1}", "SUPEREQUIPO", DateTime.Now.ToLongTimeString())
            };

            this.newTeamEventEmitter.EmitNewTeamEvent(newMemberEvent);

            return Ok();
        }
    }
}
