namespace CodeFirst.Models.Entities
{
    using System.Collections.Generic;

    public class TicketType : IEntity<int>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        #region Navigation properties

        public virtual List<Ticket> Tickets { get; set; }

        #endregion
    }
}