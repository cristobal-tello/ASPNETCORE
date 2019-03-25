using System;

namespace ASPNETCORE.Web.Models
{
    public class MemberViewModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public MemberViewModel()
        {
        }
        public MemberViewModel(Guid id) : this()
        {
            this.ID = id;
        }
        public MemberViewModel(string firstName, string lastName, Guid id) : this(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public override string ToString()
        {
            return this.LastName;
        }
    }
}