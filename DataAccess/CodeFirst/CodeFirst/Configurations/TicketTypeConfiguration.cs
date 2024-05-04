namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class TicketTypeConfiguration : EntityTypeConfiguration<TicketType>
    {
        public TicketTypeConfiguration()
        {
            ToTable("TicketType").HasKey(x => x.Id);
        }
    }
}
