namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class UserToProjectRepository : BaseRepository<UserToProject, long>, IUserToProjectRepository
    {
        public UserToProjectRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
