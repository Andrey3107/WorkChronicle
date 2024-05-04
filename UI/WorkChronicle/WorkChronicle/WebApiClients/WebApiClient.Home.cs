namespace WorkChronicle.WebApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeFirst.Models;
    using CodeFirst.Models.Entities;

    using ViewModels;

    public partial class WebApiClient
    {
        public List<TestTable> GetTestTables()
        {
            return Get<List<TestTable>>("/Table/GetList");
        }

        public Task<bool> CreateProjectAsync(CreateProjectViewMode viewModel)
        {
            return PostAsync<CreateProjectViewMode, bool>("/Project/CreateProject", viewModel);
        }

        public Task<List<Project>> GetAllProjects()
        {
            return GetAsync<List<Project>>("/Project/GetAll");
        }
    }
}
