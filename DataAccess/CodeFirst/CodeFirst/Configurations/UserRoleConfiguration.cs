namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            ToTable("UserRole").HasKey(p => p.Id);

            HasRequired(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
            HasRequired(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
        }
    }
}
