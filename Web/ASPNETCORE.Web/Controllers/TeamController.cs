using ASPNETCORE.Web.Client.Interfaces;
using ASPNETCORE.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASPNETCORE.Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamClient teamClient;

        public TeamController (ITeamClient teamClient)
        {
            this.teamClient = teamClient;
        }
        
        // GET: Default
        public async Task<ActionResult> Index()
        {
            return View(await teamClient.GetTeamsAsync());
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID")] TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {

                await teamClient.AddTeamAsync(
                    new Team()
                    {
                        ID = teamViewModel.ID,
                        Name = teamViewModel.Name
                    });

                return RedirectToAction(nameof(Index));
            }
            return View(teamViewModel);
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}