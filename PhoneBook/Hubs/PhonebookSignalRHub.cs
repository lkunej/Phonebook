using Microsoft.AspNet.SignalR;

namespace PhoneBookNamespace.Hubs
{
    public class PhonebookSignalRHub : Hub
    {
        public void SendNotification(string message)
        {
            Clients.All.sendNotification(message);
        }
    }
}