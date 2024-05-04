namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class TimeTrackRepository : BaseRepository<TimeTrack, long>, ITimeTrackRepository
    {
        public TimeTrackRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
