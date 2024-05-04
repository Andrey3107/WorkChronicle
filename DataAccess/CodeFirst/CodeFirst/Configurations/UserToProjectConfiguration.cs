namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class UserToProjectConfiguration : EntityTypeConfiguration<UserToProject>
    {
        public UserToProjectConfiguration()
        {
            ToTable("UserToProject").HasKey(x => x.Id);

            HasRequired(x => x.User).WithMany(x => x.UserToProjects).HasForeignKey(x => x.UserId);
            HasRequired(x => x.Project).WithMany(x => x.UserToProjects).HasForeignKey(x => x.ProjectId);
        }
    }
}
