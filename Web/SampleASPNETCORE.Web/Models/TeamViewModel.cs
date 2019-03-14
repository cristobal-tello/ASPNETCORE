using System;
using System.Collections.Generic;

namespace ASPNETCORE.Web.Models
{
    public class TeamViewModel
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public ICollection<MemberViewModel> Members { get; set; }
        public TeamViewModel()
        {
            this.Members = new List<MemberViewModel>();
        }
        public TeamViewModel(string name) : this()
        {
            this.Name = name;
        }
        public TeamViewModel(string name, Guid id) : this(name)
        {
            this.ID = id;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}