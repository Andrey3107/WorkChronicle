namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class PositionRepository : BaseRepository<Position, long>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
