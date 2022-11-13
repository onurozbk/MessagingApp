using MessagingApp.UI.Models.MongoDbModels;

namespace MessagingApp.UI.Business.Abstract
{
    public interface IRoomService
    {
        void Add(string roomName, string userId);
        List<Room> GetAll();
    }

}
