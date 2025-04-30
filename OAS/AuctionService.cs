using System;
using System.Collections.Generic;
using System.Linq;

namespace OAS
{
    public class AuctionService
    {
        private List<AuctionItem> auctionItems;

        public AuctionService()
        {
            auctionItems = new List<AuctionItem>();
        }

        public AuctionItem GetAuctionItemById(int itemId)
        {
            return auctionItems.FirstOrDefault(item => item.ItemId == itemId);
        }

        public void AddAuctionItem(string title, string description, decimal startingPrice)
        {
            var newItem = new AuctionItem(title, description, startingPrice);
            auctionItems.Add(newItem);
            Console.WriteLine($"Item '{title}' added to auction with starting price {startingPrice:C}.");
        }

        public void ListAuctionItems()
        {
            Console.WriteLine("\n=== Current Auction Items ===");
            foreach (var item in auctionItems.Where(i => !i.IsSold))
            {
                Console.WriteLine(item);
                Console.WriteLine("-------------------------");
            }
        }

        public AuctionItem GetItemById(int itemId)
        {
            return auctionItems.FirstOrDefault(item => item.ItemId == itemId);
        }

        public bool PlaceBid(int itemId, User bidder, decimal bidAmount)
        {
            var item = GetItemById(itemId);
            if (item == null)
            {
                Console.WriteLine("Auction item not found.");
                return false;
            }

            if (item.IsSold)
            {
                Console.WriteLine("Item has already been sold.");
                return false;
            }

            return item.PlaceBid(bidder, bidAmount);
        }

        public void CloseAuction(int itemId)
        {
            var item = GetItemById(itemId);
            if (item != null && !item.IsSold)
            {
                item.MarkAsSold();
            }
            else
            {
                Console.WriteLine("Item not found or already sold.");
            }
        }

        public List<AuctionItem> GetAllItems()
        {
            return auctionItems;
        }

        public AuctionItem GetLastAddedItem()
        {
            if (auctionItems.Count > 0)
                return auctionItems[auctionItems.Count - 1];
            return null;
        }

        // ✅✅✅ --- IMPROVEMENT: List with Countdown Timers ---
        public void ListAuctionItemsWithTimer()
        {
            Console.WriteLine("\n=== Current Auction Items ===");

            foreach (var item in auctionItems.Where(i => !i.IsSold))
            {
                Console.WriteLine(item);

                if (item.EndTime.HasValue)
                {
                    TimeSpan remaining = item.EndTime.Value - DateTime.Now;
                    if (remaining.TotalSeconds > 0)
                        Console.WriteLine($"⏳ Time Left: {remaining.Minutes:D2}:{remaining.Seconds:D2}");
                    else
                        Console.WriteLine("❌ Auction Ended (processing winner...)");
                }

                Console.WriteLine("-------------------------");
            }
        }
    }
}
