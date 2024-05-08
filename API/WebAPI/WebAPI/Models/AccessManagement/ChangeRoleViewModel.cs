namespace WebAPI.Models.AccessManagement
{
    using System.Collections.Generic;

    using CodeFirst.Models.Entities;

    public class ChangeRoleViewModel
    {
        public long UserId { get; set; }

        public string UserEmail { get; set; }

        public List<Role> AllRoles { get; set; }

        public IList<string> UserRoles { get; set; }

        public ChangeRoleViewModel()
        {
            AllRoles = new List<Role>();
            UserRoles = new List<string>();
        }
    }
}
