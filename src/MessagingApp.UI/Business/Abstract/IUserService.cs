using MessagingApp.UI.Models.MongoDbModels;

namespace MessagingApp.UI.Business.Abstract
{
    public interface IUserService
    {
        User? GetUserById(string uid);
        User? GetUserByNickName(string nickName);
        Task<User> Add(string nickName);
    }
}
