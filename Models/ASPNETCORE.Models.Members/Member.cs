using System;

namespace ASPNETCORE.Models
{
    public class Member
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid TeamID { get; set; }

        public Member()
        {
        }
        public Member(Guid id) : this()
        {
            this.ID = id;
        }
        public Member(string firstName, string lastName, Guid id, Guid teamId) : this(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.TeamID = teamId;
        }
        public override string ToString()
        {
            return this.LastName;
        }
    }
}
