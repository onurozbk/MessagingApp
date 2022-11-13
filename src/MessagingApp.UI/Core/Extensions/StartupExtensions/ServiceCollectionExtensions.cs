using MessagingApp.UI.Core.Models.Concrete;

namespace MessagingApp.UI.Core.Extensions.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<MongoDbSettings>(options =>
            {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");

                options.ConnectionString = $"mongodb://{dbHost}:27017";
                options.Database = dbName;
            });
        }
    }
}
