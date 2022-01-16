using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Services
{
    public class NotificationSystem
    {
        public void SendNotification(List<Client> clients, string message)
        {
            foreach (Client client in clients)
            {
                client.Notifications.Add(message);
            }
        }
    }
}