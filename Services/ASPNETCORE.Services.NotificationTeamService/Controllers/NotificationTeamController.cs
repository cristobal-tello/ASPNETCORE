using ASPNETCORE.Infrastructure.Notifications.Emitters.EventData;
using ASPNETCORE.Infrastructure.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ASPNETCORE.Services.NotificationTeamService.Controllers
{
    [Route("services/[controller]")]
    [ApiController]
    public class NotificationTeamController : Controller
    {
        private readonly INewTeamEventEmitter newTeamEventEmitter;

        public NotificationTeamController(INewTeamEventEmitter eventEmitter)
        {
            this.newTeamEventEmitter = eventEmitter;
        }


        [HttpPost]
        public IActionResult NewTeam([FromBody] String team)
        {
            /*    Stream req = Request.Body;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();*/

            NewTeamEventData newMemberEvent = new NewTeamEventData()
            {
                Name = string.Format("{0} at {1}", team == null ? "VACIO" : team, DateTime.Now.ToLongTimeString())
            };

            this.newTeamEventEmitter.EmitNewTeamEvent(newMemberEvent);


            return Ok();
        }
    }
}
