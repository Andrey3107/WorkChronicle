namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class PriorityRepository : BaseRepository<Priority, int>, IPriorityRepository
    {
        public PriorityRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
