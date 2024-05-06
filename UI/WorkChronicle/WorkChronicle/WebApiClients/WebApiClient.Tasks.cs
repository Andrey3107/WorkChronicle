namespace WorkChronicle.WebApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    public partial class WebApiClient
    {
        public Task<List<TicketType>> GetTicketTypes()
        {
            return GetAsync<List<TicketType>>("/TicketType/GetTicketTypes");
        }

        public Task<List<TicketStatus>> GetTicketStatuses()
        {
            return GetAsync<List<TicketStatus>>("/TicketStatus/GetTicketStatuses");
        }

        public Task<List<Priority>> GetPriorities()
        {
            return GetAsync<List<Priority>>("/Priority/GetPriorities");
        }

        public Task<bool> CreateTicket(Ticket ticket)
        {
            return PostAsync<Ticket, bool>("/Ticket/CreateTicket", ticket);
        }

        public Task<List<User>> GetAssigneesByProjectId(long projectId)
        {
            return GetAsync<List<User>>($"/User/GetUsersByProject?projectId={projectId}");
        }
    }
}
