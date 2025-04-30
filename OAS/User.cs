using System.Collections.Generic;

namespace OAS
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<AuctionItem> WonItems { get; set; } // 🛠️ Made set; public
        public decimal Balance { get; set; }

        private static int _idCounter = 1;

        public User(string username, string password)
        {
            UserId = _idCounter++;
            Username = username;
            Password = password;
            Balance = 0.0m; // Start with zero balance
            WonItems = new List<AuctionItem>(); // ✅ Initialize WonItems here!
        }

        public bool Authenticate(string username, string password)
        {
            return Username == username && Password == password;
        }

        public void AddFunds(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"{amount:C} added to your balance. New balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        public void DeductFunds(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"{amount:C} deducted from your balance. New balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public override string ToString()
        {
            return $"User ID: {UserId}, Username: {Username}, Balance: {Balance:C}";
        }
    }
}
