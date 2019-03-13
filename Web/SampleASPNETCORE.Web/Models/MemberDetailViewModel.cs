using ASPNETCORE.Models;
using System;

namespace ASPNETCORE.Web.Models
{
    public class MemberDetaillViewModel : Member
    {
        public MemberDetaillViewModel(String teamName, Member m) : base(m.FirstName, m.LastName, m.ID)
        {
            this.TeamName = teamName;
        }

        public MemberDetaillViewModel()
        {
        }

        public String TeamName { get; set; }
    }
}
