using MessagingApp.UI.Business.Abstract;
using MessagingApp.UI.Cache.Abstract;
using MessagingApp.UI.DataAccess.Abstract;
using MessagingApp.UI.Models.MongoDbModels;

namespace MessagingApp.UI.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ICacheService _cache;
        public UserManager(
            IUserDal userDal,
            ICacheService cache
            )
        {
            this._userDal = userDal;
            this._cache = cache;
        }

        public User? GetUserById(string userId)
        {
            var user = _cache.GetOrAdd<User>("user:" + userId, () => { return _userDal.Get(x => x.Id == userId).FirstOrDefault(); });
            return user;
        }

        public User? GetUserByNickName(string nickName)
        {
            return _userDal.Get(x => x.NickName == nickName).FirstOrDefault();
        }

        public async Task<User> Add(string nickName)
        {
            var user = await _userDal.AddAsync(new User() { NickName = nickName });
            _cache.GetOrAdd<User>("user:" + user.Id, () => { return user; });
            return user;
        }
    }
}
