using System;

namespace OAS
{
    public class Bid
    {
        public User Bidder { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime TimePlaced { get; private set; }

        public Bid(User bidder, decimal amount)
        {
            Bidder = bidder;
            Amount = amount;
            TimePlaced = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Bidder: {Bidder.Username}, Amount: {Amount:C}, Time: {TimePlaced}";
        }
    }
}
