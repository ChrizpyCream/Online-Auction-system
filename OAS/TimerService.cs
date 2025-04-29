using System;
using System.Collections.Generic;
using System.Threading;
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
                Console.WriteLine($"Auction for '{item.Title}' will end in {durationSeconds} seconds...");
                await Task.Delay(durationSeconds * 1000);

                if (!item.IsSold)
                {
                    if (item.HighestBidder != null)
                    {
                        item.MarkAsSold();
                        _notificationService.NotifyUser(item.HighestBidder, $"Congratulations! You won '{item.Title}' for {item.CurrentHighestBid:C}.");
                    }
                    else
                    {
                        Console.WriteLine($"Auction for '{item.Title}' ended with no bids.");
                    }
                }
            });
        }
    }
}
