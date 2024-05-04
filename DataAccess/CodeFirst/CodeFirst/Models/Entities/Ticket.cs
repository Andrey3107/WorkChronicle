namespace CodeFirst.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Ticket : IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public double? Estimate { get; set; }

        public DateTime? DueDate { get; set; }

        public int TicketStatusId { get; set; }

        public int? Completeness { get; set; }

        public long ProjectId { get; set; }

        public int? TypeId { get; set; }

        public int PriorityId { get; set; }

        public long AssigneeId { get; set; }

        #region Navigation properties

        public virtual TicketStatus TicketStatus { get; set; }

        public virtual Project Project { get; set; }

        public virtual TicketType TicketType { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual User Assignee { get; set; }

        public virtual List<TimeTrack> TimeTracks { get; set; }

        #endregion
    }
}
