using MessagingApp.UI.Business.Abstract;
using MessagingApp.UI.Business.Concrete;
using MessagingApp.UI.Cache.Abstract;
using MessagingApp.UI.Cache.Concrete;
using MessagingApp.UI.Core.Extensions.StartupExtensions;
using MessagingApp.UI.DataAccess.Abstract;
using MessagingApp.UI.DataAccess.Concrete.MongoDb;
using MessagingApp.UI.Hubs;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMongoDbSettings(builder.Configuration);

builder.Services.AddScoped<IUserDal, UserMongoDbDal>();
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddScoped<IRoomDal, RoomMongoDbDal>();
builder.Services.AddScoped<IRoomService, RoomManager>();

builder.Services.AddScoped<IRoomUserDal, RoomUserMongoDbDal>();
builder.Services.AddScoped<IRoomUserService, RoomUserManager>();

builder.Services.AddScoped<IMessageDal, MessageMongoDbDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<ICacheService, RedisCacheService>();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_CONNECTION") ?? ""));

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<RealTimeChatHub>("/realTimeChatHub");
    endpoints.MapDefaultControllerRoute();
});

app.Run();
