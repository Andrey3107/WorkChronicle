namespace CodeFirst.Repositories.WorkChronicle
{
    using Interfaces.WorkChronicle;

    using Models.Entities;

    public class PlaceRepository : BaseRepository<Place, int>, IPlaceRepository
    {
        public PlaceRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
