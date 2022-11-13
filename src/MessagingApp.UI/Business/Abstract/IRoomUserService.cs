using MessagingApp.UI.Models.MongoDbModels;

namespace MessagingApp.UI.Business.Abstract
{
    public interface IRoomUserService
    {
        List<RoomUser> GetUserRooms(string userId);
        void JoinToRoom(string userId, string roomId);
    }

}
