using System;
using System.Threading.Tasks;

namespace OAS
{
    public class TimerService
    {
        private readonly AuctionService _auctionService;
        private readonly NotificationService _notificationService;

        public TimerService(AuctionService auctionService, NotificationService notificationService)
        {
            _auctionService = auctionService;
            _notificationService = notificationService;
        }

        public void StartAuctionTimer(AuctionItem item, int durationSeconds)
        {
            Task.Run(async () =>
            {
                Console.WriteLine($"⏳ Auction for '{item.Title}' will end in {durationSeconds} seconds...");
                await Task.Delay(durationSeconds * 1000); // Silent wait

                if (!item.IsSold)
                {
                    if (item.HighestBidder != null)
                    {
                        item.MarkAsSold();
                        Console.WriteLine($"🏁 Auction for '{item.Title}' is over! Winner: {item.HighestBidder.Username} with a bid of {item.CurrentHighestBid:C}.");

                        _notificationService.NotifyUser(item.HighestBidder, $"🎉 Congratulations! You won '{item.Title}' for {item.CurrentHighestBid:C}.");

                        // ✅ Safely add the item to the user's WonItems list
                        if (item.HighestBidder.WonItems == null)
                        {
                            item.HighestBidder.WonItems = new List<AuctionItem>();
                        }
                        item.HighestBidder.WonItems.Add(item);
                    }
                    else
                    {
                        Console.WriteLine($"🚫 Auction for '{item.Title}' ended with no bids.");
                    }
                }
            });
        }
    }
}
