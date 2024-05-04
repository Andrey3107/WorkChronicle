namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class ProjectStatusRepository : BaseRepository<ProjectStatus, int>, IProjectStatusRepository
    {
        public ProjectStatusRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
