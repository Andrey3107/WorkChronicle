namespace WorkChronicle.WebApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using ViewModels.Tasks;

    public partial class WebApiClient
    {
        public Task<List<TicketType>> GetTicketTypes()
        {
            return GetAsync<List<TicketType>>("/TicketType/GetTicketTypes");
        }

        public Task<Ticket> GetTicketById(long id)
        {
            return GetAsync<Ticket>($"/Ticket/GetTicketById?id={id}");
        }

        public Task<List<TicketStatus>> GetTicketStatuses()
        {
            return GetAsync<List<TicketStatus>>("/TicketStatus/GetTicketStatuses");
        }

        public Task<List<Place>> GetPlaces()
        {
            return GetAsync<List<Place>>("/Place/GetPlaces");
        }

        public Task<List<Priority>> GetPriorities()
        {
            return GetAsync<List<Priority>>("/Priority/GetPriorities");
        }

        public Task<List<TicketViewModel>> GetTicketsByUser(long userId)
        {
            return GetAsync<List<TicketViewModel>>($"/Ticket/GetTicketsByUser?userId={userId}");
        }

        public Task<bool> CreateTicket(Ticket ticket)
        {
            return PostAsync<Ticket, bool>("/Ticket/CreateTicket", ticket);
        }

        public Task<bool> CreateTimeTrack(TimeTrack timeTrack)
        {
            return PostAsync<TimeTrack, bool>("/TimeTrack/CreateTimeTrack", timeTrack);
        }

        public Task<TicketModalViewModel> GetTicketDetailsById(long id)
        {
            return GetAsync<TicketModalViewModel>($"/Ticket/GetTicketDetailsById?id={id}");
        }

        public Task<List<User>> GetAssigneesByProjectId(long projectId)
        {
            return GetAsync<List<User>>($"/User/GetUsersByProject?projectId={projectId}");
        }
    }
}
