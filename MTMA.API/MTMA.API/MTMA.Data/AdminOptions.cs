namespace MTMA.Data
{
    public class AdminOptions
    {
        public const string Admin = "Admin";

        required public string Email { get; set; }

        required public string UserName { get; set; }

        required public string Password { get; set; }
    }
}
