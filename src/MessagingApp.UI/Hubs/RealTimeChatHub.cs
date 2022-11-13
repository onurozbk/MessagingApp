using Microsoft.AspNetCore.SignalR;

namespace MessagingApp.UI.Hubs
{
    public class RealTimeChatHub : Hub
    {
        public async Task SendNewRoomPush()
        {
            await Clients.All.SendAsync("NewRoomAdded");
        }

        public async Task SendNewMessagePush()
        {
            await Clients.All.SendAsync("GetRoomMessage");
        }
    }
}
