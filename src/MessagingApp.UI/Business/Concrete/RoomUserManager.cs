using MessagingApp.UI.Business.Abstract;
using MessagingApp.UI.Cache.Abstract;
using MessagingApp.UI.DataAccess.Abstract;
using MessagingApp.UI.Models.MongoDbModels;

namespace MessagingApp.UI.Business.Concrete
{
    public class RoomUserManager : IRoomUserService
    {
        private readonly IRoomUserDal _roomUserDal;
        private readonly ICacheService _cache;
        public RoomUserManager(
             IRoomUserDal roomUserDal,
             ICacheService cache
            )
        {
            this._roomUserDal = roomUserDal;
            this._cache = cache;
        }

        public List<RoomUser> GetUserRooms(string userId)
        {
            return _cache.GetOrAdd("userRooms:" + userId, () => { return _roomUserDal.Get(x => x.UserId == userId).ToList(); });
        }
        public void JoinToRoom(string userId, string roomId)
        {
            _cache.Clear("userRooms:" + userId);
            _roomUserDal.AddAsync(new RoomUser() { RoomId = roomId, UserId = userId });
        }
    }
}
