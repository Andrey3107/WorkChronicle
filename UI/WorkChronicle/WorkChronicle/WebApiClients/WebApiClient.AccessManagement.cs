namespace WorkChronicle.WebApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeFirst.Models.Entities;

    using ViewModels.AccessManagement;

    public partial class WebApiClient
    {
        public Task<List<User>> GetAllUsers()
        {
            return GetAsync<List<User>>($"/User/GetAllUsers");
        }

        public Task<ChangeRoleViewModel> GetUserPermissions(long userId)
        {
            return GetAsync<ChangeRoleViewModel>($"/User/GetUserPermissions?userId={userId}");
        }

        public Task<bool> ChangeUserPermissions(ChangeRoleViewModel filter)
        {
            return PostAsync<ChangeRoleViewModel, bool>("/User/ChangeUserPermissions", filter);
        }
    }
}
