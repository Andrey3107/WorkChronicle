namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class ProjectStatusConfiguration : EntityTypeConfiguration<ProjectStatus>
    {
        public ProjectStatusConfiguration()
        {
            ToTable("ProjectStatus").HasKey(x => x.Id);
        }
    }
}
