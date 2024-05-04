namespace CodeFirst.Interfaces.WorkChronicle
{
    using Models.Entities;

    public interface ITicketRepository : IBaseRepository<Ticket, long>
    {
    }
}
