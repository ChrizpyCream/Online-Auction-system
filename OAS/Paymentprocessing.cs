using System;

namespace OAS
{
    public class PaymentService
    {
        public bool ProcessPayment(User buyer, decimal amount)
        {
            if (buyer == null)
            {
                Console.WriteLine("Payment failed: Buyer not found.");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Payment failed: Amount must be greater than zero.");
                return false;
            }

            if (buyer.Balance >= amount)
            {
                buyer.DeductFunds(amount);
                Console.WriteLine($"Payment of {amount:C} processed successfully for {buyer.Username}.");
                return true;
            }
            else
            {
                Console.WriteLine("Payment failed: Insufficient funds.");
                return false;
            }
        }

        public void Refund(User buyer, decimal amount)
        {
            if (buyer == null)
            {
                Console.WriteLine("Refund failed: Buyer not found.");
                return;
            }

            if (amount > 0)
            {
                buyer.AddFunds(amount);
                Console.WriteLine($"Refund of {amount:C} issued to {buyer.Username}.");
            }
            else
            {
                Console.WriteLine("Refund failed: Amount must be greater than zero.");
            }
        }
    }
}
