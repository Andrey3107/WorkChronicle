namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class PriorityConfiguration : EntityTypeConfiguration<Priority>
    {
        public PriorityConfiguration()
        {
            ToTable("Priority").HasKey(x => x.Id);
        }
    }
}
