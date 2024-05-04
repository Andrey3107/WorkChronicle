namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class TicketStatusConfiguration : EntityTypeConfiguration<TicketStatus>
    {
        public TicketStatusConfiguration()
        {
            ToTable("TicketStatus").HasKey(x => x.Id);
        }
    }
}
