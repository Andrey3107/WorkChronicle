namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models;

    public class TestTableConfiguration : EntityTypeConfiguration<TestTable>
    {
        public TestTableConfiguration()
        {
            ToTable("TestTable").HasKey(p => p.Id);
        }
    }
}
