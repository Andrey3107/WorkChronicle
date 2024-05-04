namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class TicketRepository : BaseRepository<Ticket, long>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
