namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User").HasKey(p => p.Id);
        }
    }
}
