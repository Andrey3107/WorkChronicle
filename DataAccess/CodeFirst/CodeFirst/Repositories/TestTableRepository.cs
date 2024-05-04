namespace CodeFirst.Repositories
{
    using Interfaces;

    using Models;

    public class TestTableRepository : BaseRepository<TestTable, long>, ITestTableRepository
    {
        public TestTableRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
