namespace WebAPI.Models.User
{
    public class UpdateProfile
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
