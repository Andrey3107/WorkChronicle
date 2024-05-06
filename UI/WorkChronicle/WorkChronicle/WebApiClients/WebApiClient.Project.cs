namespace WorkChronicle.WebApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using ViewModels;

    public partial class WebApiClient
    {
        public Task<bool> CreateProjectAsync(CreateProjectViewMode viewModel)
        {
            return PostAsync<CreateProjectViewMode, bool>("/Project/CreateProject", viewModel);
        }

        public Task<List<Project>> GetAllProjects()
        {
            return GetAsync<List<Project>>("/Project/GetAll");
        }

        public Task<Project> GetProjectById(long id)
        {
            return GetAsync<Project>($"/Project/GetProjectById?id={id}");
        }

        public Task<bool> UpdateProject(Project project)
        {
            return PostAsync<Project, bool>($"/Project/UpdateProject", project);
        }

        public Task<bool> DeactivateProject(long id)
        {
            return GetAsync<bool>($"/Project/DeactivateProject?id={id}");
        }

        public Task<bool> ActivateProject(long id)
        {
            return GetAsync<bool>($"/Project/ActivateProject?id={id}");
        }

        public Task<bool> DeleteProject(long id)
        {
            return GetAsync<bool>($"/Project/DeleteProject?id={id}");
        }
    }
}
