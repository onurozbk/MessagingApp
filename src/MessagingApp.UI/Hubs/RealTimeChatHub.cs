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

        //public async Task SendPushHelp(string helpguid, string message)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, helpguid);
        //    await Clients.Group(helpguid).SendAsync("Send", message);
        //}
    }
}
