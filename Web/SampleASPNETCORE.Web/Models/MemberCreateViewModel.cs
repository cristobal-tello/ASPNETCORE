using ASPNETCORE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCORE.Web.Models
{
    public class MemberCreateViewModel : Member
    {
        public Guid TeamId { get; set; }

        public List<SelectListItem> TeamList { get; set; }
    }
}
