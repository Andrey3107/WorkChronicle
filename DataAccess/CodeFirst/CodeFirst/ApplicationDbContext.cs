namespace CodeFirst
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Reflection;

    using Models;
    using Models.Entities;

    public class ApplicationDbContext : DbContext
    {
        private const string DefaultNameOrConnectionString = "Data Source=DESKTOP-1U5MD1I\\SQLEXPRESS;Initial Catalog=WorkChronicle;Integrated Security=True;MultipleActiveResultSets=True;";

        public ApplicationDbContext() 
            : base(DefaultNameOrConnectionString)
        {
            Database.CommandTimeout = 900;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<TestTable> TestTables { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Priority> Priorities { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectStatus> ProjectStatuses { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketStatus> TicketStatuses { get; set; }

        public DbSet<TicketType> TicketTypes { get; set; }

        public DbSet<TimeTrack> TimeTracks { get; set; }

        public DbSet<UserToProject> UserToProjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var configurations = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var configuration in configurations)
            {
                dynamic configurationInstance = Activator.CreateInstance(configuration);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
