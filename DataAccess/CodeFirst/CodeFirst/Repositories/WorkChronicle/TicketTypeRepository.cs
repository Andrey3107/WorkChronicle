namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class TicketTypeRepository : BaseRepository<TicketType, int>, ITicketTypeRepository
    {
        public TicketTypeRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
