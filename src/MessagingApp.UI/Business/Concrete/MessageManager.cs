using MessagingApp.UI.Business.Abstract;
using MessagingApp.UI.Cache.Abstract;
using MessagingApp.UI.DataAccess.Abstract;
using MessagingApp.UI.Models.DTOs;
using MessagingApp.UI.Models.MongoDbModels;
using System.Diagnostics;

namespace MessagingApp.UI.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;
        private readonly IUserDal _userDal;
        private readonly ICacheService _cache;
        public MessageManager(
            IMessageDal messageDal,
            IUserDal userDal,
            ICacheService cache
            )
        {
            this._messageDal = messageDal;
            this._userDal = userDal;
            this._cache = cache;
        }
        public List<RoomDetailMessageeDto> GetRoomMesssages(string roomId)
        {
            List<RoomDetailMessageeDto> result = new List<RoomDetailMessageeDto>();
            var roomMessages = _cache.GetOrAdd("messageRoomMessage:" + roomId, () => { return _messageDal.Get(x => x.RoomId == roomId).ToList(); });
            var users = _cache.GetOrAdd("userList", () => { return _userDal.Get().ToList(); });
            result = (from a in roomMessages
                      select new RoomDetailMessageeDto
                      {
                          Message = a.Text,
                          SaveDate = a.CreatedAt,
                          SaveUser = users.FirstOrDefault(x => x.Id == a.UserId).NickName,
                      }).ToList();
            return result;
        }

        public async void AddMessage(string mesage, string userId, string roomId)
        {
            await _cache.Clear("messageRoomMessage:" + roomId);
            await _messageDal.AddAsync(new Message()
            {
                RoomId = roomId,
                UserId = userId,
                Text = mesage
            });
        }
    }
}
