namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            ToTable("Project").HasKey(x => x.Id);

            HasRequired(x => x.ProjectStatus).WithMany(x => x.Projects).HasForeignKey(x => x.ProjectStatusId);
        }
    }
}
