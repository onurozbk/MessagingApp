using MessagingApp.UI.Models.MongoDbModels;
using MessagingApp.UI.Repository.Abstract;

namespace MessagingApp.UI.DataAccess.Abstract
{
    public interface IRoomUserDal : IRepository<RoomUser, string>
    {
    }
}
