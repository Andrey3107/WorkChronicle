namespace WebAPI.Models.Project
{
    using System.Collections.Generic;

    using CodeFirst.Models.Entities;

    public class ChangeParticipantsViewModel
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }

        public List<User> AllUsers { get; set; }

        public IList<long> ProjectUsers { get; set; }

        public ChangeParticipantsViewModel()
        {
            AllUsers = new List<User>();
            ProjectUsers = new List<long>();
        }
    }
}
