namespace WebAPI.Models.Ticket
{
    public class TicketViewModel
    {
        public long Id { get; set; }

        public string ProjectName { get; set; }

        public string TicketName { get; set; }

        public string Priority { get; set; }

        public int TicketStatusId { get; set; }
    }
}
