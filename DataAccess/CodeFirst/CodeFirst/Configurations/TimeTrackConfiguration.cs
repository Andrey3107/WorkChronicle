namespace CodeFirst.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Models.Entities;

    public class TimeTrackConfiguration : EntityTypeConfiguration<TimeTrack>
    {
        public TimeTrackConfiguration()
        {
            ToTable("TimeTrack").HasKey(x => x.Id);

            HasRequired(x => x.Ticket).WithMany(x => x.TimeTracks).HasForeignKey(x => x.TicketId);
            HasOptional(x => x.Place).WithMany(x => x.TimeTracks).HasForeignKey(x => x.PlaceId);
            HasRequired(x => x.Assignee).WithMany(x => x.TimeTracks).HasForeignKey(x => x.AssigneeId);
        }
    }
}
