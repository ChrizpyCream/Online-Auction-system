
namespace OAS
{
    public class Admin
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public bool Login(string username, string password)
        {
            return Username == username && Password == password;
        }

        public void UpdatePassword(string newPassword)
        {
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                Password = newPassword;
                Console.WriteLine("Admin password updated successfully.");
            }
            else
            {
                Console.WriteLine("Password update failed. New password cannot be empty.");
            }
        }

        public override string ToString()
        {
            return $"Admin Username: {Username}";
        }
    }
}
