using MessagingApp.UI.Models.DTOs;
using MessagingApp.UI.Models.MongoDbModels;

namespace MessagingApp.UI.Business.Abstract
{
    public interface IMessageService
    {
        List<RoomDetailMessageeDto> GetRoomMesssages(string roomId);
        void AddMessage(string mesage, string userId, string roomId);
    }
}
