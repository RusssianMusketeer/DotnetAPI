namespace DotnetAPI.Models
{
    public class Auth
    {
        public int UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Email { get; set; }

        public Auth()
        {
            if (PasswordHash == null)
            {
                PasswordHash = new byte[0];
            }
            if (PasswordSalt == null)
            {
                PasswordSalt = new byte[0];
            }
            if (Email == null)
            {
                Email = "";
            }

        }
    }
}