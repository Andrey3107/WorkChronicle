namespace CodeFirst.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class User : IEntity<long>
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime? Created { get; set; }

        public long? PositionId { get; set; }

        #region Navigation properties

        public virtual Position Position { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
        
        public virtual List<Ticket> Tickets { get; set; }

        public virtual List<TimeTrack> TimeTracks { get; set; }

        public virtual List<UserToProject> UserToProjects { get; set; }

        #endregion
    }
}
