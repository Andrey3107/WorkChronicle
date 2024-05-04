namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class UserRepository : BaseRepository<User, long>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
