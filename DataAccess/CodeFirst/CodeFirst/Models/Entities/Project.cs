namespace CodeFirst.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Project : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public int ProjectStatusId { get; set; }

        #region Navigation properties

        public virtual ProjectStatus ProjectStatus { get; set; }

        public virtual List<Ticket> Tickets { get; set; }

        public virtual List<UserToProject> UserToProjects { get; set; }

        #endregion
    }
}
