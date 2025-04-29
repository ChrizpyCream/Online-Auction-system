using System;

namespace OAS
{
    public class NotificationService
    {
        public void NotifyUser(User user, string message)
        {
            if (user == null)
            {
                Console.WriteLine("Notification failed: user not found.");
                return;
            }

            // For now, we just write to console — later you could expand to email, SMS, etc.
            Console.WriteLine($"\n--- Notification ---\nTo: {user.Username}\nMessage: {message}\n---------------------");
        }

        public void NotifyAdmin(Admin admin, string message)
        {
            if (admin == null)
            {
                Console.WriteLine("Notification failed: admin not found.");
                return;
            }

            Console.WriteLine($"\n--- Admin Notification ---\nTo: {admin.Username}\nMessage: {message}\n---------------------------");
        }
    }
}
