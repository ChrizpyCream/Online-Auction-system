using System;
using System.Collections.Generic;
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
            TimerService timerService = new TimerService(auctionService, notificationService); // << Added TimerService

            // Sample admin
            Admin admin = new Admin("admin", "admin123");

            // Sample users
            List<User> users = new List<User>
            {
                new User("user1", "password1"),
                new User("user2", "password2")
            };

            // Admin adds some sample auction items
            auctionService.AddAuctionItem("Vintage Watch", "Classic 1960s wristwatch", 100);
            AuctionItem watch = auctionService.GetLastAddedItem();
            timerService.StartAuctionTimer(watch, 30); // Auction ends in 30 seconds

            auctionService.AddAuctionItem("Gaming Laptop", "High-end specs", 1200);
            AuctionItem laptop = auctionService.GetLastAddedItem();
            timerService.StartAuctionTimer(laptop, 30); // Auction ends in 30 seconds

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. View Auction Items");
                Console.WriteLine("2. Place a Bid");
                Console.WriteLine("3. Admin Panel");
                Console.WriteLine("4. Exit");

                Console.Write("\nChoice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        auctionService.ListAuctionItems();
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

                        auctionService.ListAuctionItems();
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
                                timerService.StartAuctionTimer(newItem, 30); // New items also get 30-second auctions
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
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
