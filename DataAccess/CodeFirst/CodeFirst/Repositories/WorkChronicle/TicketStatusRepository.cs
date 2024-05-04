namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class TicketStatusRepository : BaseRepository<TicketStatus, int>, ITicketStatusRepository
    {
        public TicketStatusRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
