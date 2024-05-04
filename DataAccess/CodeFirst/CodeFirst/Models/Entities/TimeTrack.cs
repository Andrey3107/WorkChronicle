namespace CodeFirst.Models.Entities
{
    public class TimeTrack : IEntity<long>
    {
        public long Id { get; set; }

        public double Duration { get; set; }

        public string Comment { get; set; }

        public long TicketId { get; set; }

        public int? PlaceId { get; set; }

        public long AssigneeId { get; set; }

        #region Navigation properties

        public virtual Ticket Ticket { get; set; }

        public virtual Place Place { get; set; }

        public virtual User Assignee { get; set; }

        #endregion
    }
}
