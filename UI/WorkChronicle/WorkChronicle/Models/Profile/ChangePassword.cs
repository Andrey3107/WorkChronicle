namespace WorkChronicle.Models.Profile
{
    public class ChangePassword
    {
        public long UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
