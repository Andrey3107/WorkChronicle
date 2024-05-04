namespace OnlineShop.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
