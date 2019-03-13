using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETCORE.Models;
using ASPNETCORE.Repository;
using ASPNETCORE.Web.Models;

namespace ASPNETCORE.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly TeamDbContext teamDbContext;

        public MemberController(TeamDbContext context)
        {
            teamDbContext = context;
        }

        // GET: Member
        public IActionResult Index()
        {
            List<MemberDetaillViewModel> memberViewModelList = new List<MemberDetaillViewModel>();

            foreach (Team t in teamDbContext.Team.Include(m=>m.Members))
            {
                foreach(Member m in t.Members)
                {
                    memberViewModelList.Add(new MemberDetaillViewModel(t.Name, m));
                }
            }

            return View(memberViewModelList);
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (Guid.Empty != null)
            {
                var member = await teamDbContext.Member.FindAsync(id);
                if (member != null)
                {
                    return View(member);
                }
            }
            return NotFound();
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            MemberCreateViewModel memberView = new MemberCreateViewModel()
            {
                TeamList =
                teamDbContext.Team.Select(
                    t => new SelectListItem()
                    {
                        Value = t.ID.ToString(),
                        Text = t.Name
                    }
                    ).ToList()
            };
                
            return View(memberView);
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, TeamId, FirstName, LastName")] MemberCreateViewModel member)
        {
            if (ModelState.IsValid)
            {
                Team team = await teamDbContext.Team.FindAsync(member.TeamId);
                if(team!=null)
                {
                    team.Members.Add(new Member(member.FirstName, member.LastName, Guid.NewGuid()));
                }
                await teamDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (Guid.Empty == id)
            {
                return NotFound();
            }

            var member = await teamDbContext.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,FirstName,LastName")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    teamDbContext.Update(member);
                    await teamDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await teamDbContext.Member
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var member = await teamDbContext.Member.FindAsync(id);
            teamDbContext.Member.Remove(member);
            await teamDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(Guid id)
        {
            return teamDbContext.Member.Any(e => e.ID == id);
        }
    }
}
