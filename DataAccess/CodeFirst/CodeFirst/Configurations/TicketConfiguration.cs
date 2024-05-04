namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class TicketConfiguration : EntityTypeConfiguration<Ticket>
    {
        public TicketConfiguration()
        {
            ToTable("Ticket").HasKey(x => x.Id);

            HasRequired(x => x.TicketStatus).WithMany(x => x.Tickets).HasForeignKey(x => x.TicketStatusId);
            HasRequired(x => x.Project).WithMany(x => x.Tickets).HasForeignKey(x => x.ProjectId);
            HasRequired(x => x.Assignee).WithMany(x => x.Tickets).HasForeignKey(x => x.AssigneeId).WillCascadeOnDelete(false);
            HasOptional(x => x.TicketType).WithMany(x => x.Tickets).HasForeignKey(x => x.TypeId);
            HasRequired(x => x.Priority).WithMany(x => x.Tickets).HasForeignKey(x => x.PriorityId);
        }
    }
}
