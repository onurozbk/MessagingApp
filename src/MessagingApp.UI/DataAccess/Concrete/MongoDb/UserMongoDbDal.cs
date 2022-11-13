using MessagingApp.UI.Core.Models.Concrete;
using MessagingApp.UI.DataAccess.Abstract;
using MessagingApp.UI.Models.MongoDbModels;
using MessagingApp.UI.Repository.Concrete;
using Microsoft.Extensions.Options;

namespace MessagingApp.UI.DataAccess.Concrete.MongoDb
{
    public class UserMongoDbDal : MongoDbRepositoryBase<User>, IUserDal
    {
        public UserMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
