namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class UserRoleRepository : BaseRepository<UserRole, long>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
