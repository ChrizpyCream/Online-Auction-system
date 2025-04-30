using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OAS;

namespace OAS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to the Online Auction System ===\n");

            AuctionService auctionService = new AuctionService();
            NotificationService notificationService = new NotificationService();
            PaymentService paymentService = new PaymentService();
            TimerService timerService = new TimerService(auctionService, notificationService);

            Admin admin = new Admin("admin", "admin123");

            List<User> users = new List<User>
            {
                new User("user1", "password1"),
                new User("user2", "password2")
            };

            // Admin adds sample auction items
            auctionService.AddAuctionItem("Vintage Watch", "Classic 1960s wristwatch⌚", 100);
            AuctionItem watch = auctionService.GetLastAddedItem();
            watch.EndTime = DateTime.Now.AddSeconds(180);
            timerService.StartAuctionTimer(watch, 180);

            auctionService.AddAuctionItem("Gaming Laptop", "High-end specs💻", 1200);
            AuctionItem laptop = auctionService.GetLastAddedItem();
            laptop.EndTime = DateTime.Now.AddSeconds(180);
            timerService.StartAuctionTimer(laptop, 180);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. View Auction Items 👀");
                Console.WriteLine("2. Place a Bid");
                Console.WriteLine("3. Admin Panel 🎟️");
                Console.WriteLine("4. Exit ❌");

                Console.Write("\nChoice: ");
                string choice = Console.ReadLine();

                Console.Clear(); // 👈 Clear AFTER user input, no lag

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("=== Current Auction Items ===");
                        auctionService.ListAuctionItemsWithTimer();
                        break;

                    case "2":
                        Console.Write("\nEnter your username: ");
                        string username = Console.ReadLine();
                        User biddingUser = users.Find(u => u.Username == username);

                        if (biddingUser == null)
                        {
                            Console.WriteLine("User not found.");
                            break;
                        }

                        Console.WriteLine("\n=== Current Auction Items ===");
                        auctionService.ListAuctionItemsWithTimer();
                        Console.Write("\nEnter Item ID to bid on: ");
                        if (int.TryParse(Console.ReadLine(), out int itemId))
                        {
                            AuctionItem item = auctionService.GetAuctionItemById(itemId);
                            if (item != null)
                            {
                                Console.Write("Enter your bid amount: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal bidAmount))
                                {
                                    auctionService.PlaceBid(itemId, biddingUser, bidAmount);
                                    notificationService.NotifyUser(biddingUser, $"You placed a bid of ${bidAmount} on '{item.Title}'.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid bid amount.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Auction item not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid item ID.");
                        }
                        break;

                    case "3":
                        Console.Write("\nEnter admin username: ");
                        string adminUsername = Console.ReadLine();
                        Console.Write("Enter admin password: ");
                        string adminPassword = Console.ReadLine();

                        if (admin.Login(adminUsername, adminPassword))
                        {
                            Console.WriteLine("\nAdmin Login Successful.");
                            Console.Write("Enter new item title: ");
                            string title = Console.ReadLine();
                            Console.Write("Enter item description: ");
                            string description = Console.ReadLine();
                            Console.Write("Enter starting price: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal price))
                            {
                                auctionService.AddAuctionItem(title, description, price);
                                AuctionItem newItem = auctionService.GetLastAddedItem();
                                newItem.EndTime = DateTime.Now.AddSeconds(180);
                                timerService.StartAuctionTimer(newItem, 180);
                                Console.WriteLine("Auction item added successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid price input.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid admin credentials.");
                        }
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("Exiting system. Goodbye!");

                        // ✅ After exiting, show all users' Won Items
                        Console.WriteLine("\n=== Auction Results ===");
                        foreach (var user in users)
                        {
                            if (user.WonItems.Count > 0)
                            {
                                Console.WriteLine($"\n{user.Username} won:");
                                foreach (var wonItem in user.WonItems)
                                {
                                    Console.WriteLine($"- {wonItem.Title} for {wonItem.CurrentHighestBid:C}");
                                }
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
