using MessagingApp.UI.Business.Abstract;
using MessagingApp.UI.Cache.Abstract;
using MessagingApp.UI.DataAccess.Abstract;
using MessagingApp.UI.Models.MongoDbModels;
using Microsoft.Extensions.Configuration.UserSecrets;
using MongoDB.Driver;

namespace MessagingApp.UI.Business.Concrete
{
    public class RoomManager : IRoomService
    {
        private readonly IRoomDal _roomDal;
        private readonly IRoomUserDal _roomUserDal;
        private readonly ICacheService _cache;
        public RoomManager(
             IRoomDal roomDal,
             IRoomUserDal roomUserDal,
             ICacheService cache
            )
        {
            this._roomDal = roomDal;
            this._roomUserDal = roomUserDal;
            this._cache = cache;
        }

        public async void Add(string roomName, string userId)
        {
            _cache.Clear("roomList");
            var room = await _roomDal.AddAsync(new Room() { Name = roomName });
            await _cache.SetValueAsync<Room>("room:" + room.Id, room);

            if (!string.IsNullOrEmpty(userId))
            {
                await _roomUserDal.AddAsync(new RoomUser() { RoomId = room.Id, UserId = userId });
                var userRooms = _roomUserDal.Get(x => x.UserId == userId).ToList();
                await _cache.SetValueAsync<List<RoomUser>>("userRooms:" + userId, userRooms);
            }
        }

        public List<Room> GetAll()
        {
            return _cache.GetOrAdd<List<Room>>("roomList", () => { return _roomDal.Get().ToList(); });
        }
    }
}
