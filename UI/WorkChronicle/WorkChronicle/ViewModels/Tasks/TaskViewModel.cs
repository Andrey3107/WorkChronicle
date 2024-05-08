namespace WorkChronicle.ViewModels.Tasks
{
    using System;

    public class TaskViewModel
    {
        public long Id { get; set; }

        public long ProjectId { get; set; }

        public int TicketTypeId { get; set; }

        public int TicketStatusId { get; set; }

        public string TaskName { get; set; }

        public string Description { get; set; }

        public long AssigneeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Estimate { get; set; }

        public int Completeness { get; set; }

        public int PriorityId { get; set; }
    }
}
