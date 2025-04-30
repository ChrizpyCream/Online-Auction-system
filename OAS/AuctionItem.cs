using System;

namespace OAS
{
    public class AuctionItem
    {
        public int ItemId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal StartingPrice { get; private set; }
        public decimal CurrentHighestBid { get; private set; }
        public User HighestBidder { get; private set; }
        public bool IsSold { get; private set; }
        public DateTime? EndTime { get; set; } // Add this to AuctionItem class


        private static int _idCounter = 1;

        public AuctionItem(string title, string description, decimal startingPrice)
        {
            ItemId = _idCounter++;
            Title = title;
            Description = description;
            StartingPrice = startingPrice;
            CurrentHighestBid = startingPrice;
            IsSold = false;
            

        }
       

        public bool PlaceBid(User bidder, decimal bidAmount)
        {
            if (bidAmount > CurrentHighestBid)
            {
                CurrentHighestBid = bidAmount;
                HighestBidder = bidder;
                Console.WriteLine($"New highest bid of {bidAmount:C} placed by {bidder.Username}!");
                return true;
            }
            else
            {
                Console.WriteLine("Bid must be higher than the current highest bid.");
                return false;
            }
        }

        public void MarkAsSold()
        {
            IsSold = true;
            Console.WriteLine($"Item '{Title}' sold to {HighestBidder?.Username} for {CurrentHighestBid:C}!");
        }

        public override string ToString()
        {
            return $"Item ID: {ItemId}\nTitle: {Title}\nDescription: {Description}\nStarting Price: {StartingPrice:C}\nCurrent Highest Bid: {CurrentHighestBid:C}\nSold: {IsSold}";
        }
    }
    

}