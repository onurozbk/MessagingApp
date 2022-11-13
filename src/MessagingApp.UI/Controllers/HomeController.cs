using MessagingApp.UI.Business.Abstract;
using MessagingApp.UI.Models;
using MessagingApp.UI.Models.MongoDbModels;
using MessagingApp.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessagingApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;
        private readonly IMessageService _messageService;
        private readonly IRoomUserService _roomUserService;
        public HomeController(
            IHttpContextAccessor contextAccessor,
            IUserService userService,
            IRoomService roomService,
            IMessageService messageService,
            IRoomUserService roomUserService
            )
        {
            this._contextAccessor = contextAccessor;
            this._userService = userService;
            this._roomService = roomService;
            this._roomUserService = roomUserService;
            this._messageService = messageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = (_contextAccessor.HttpContext.Request.Cookies["rtca-uid"] ?? "");
            if (string.IsNullOrEmpty(userId) || _userService.GetUserById(userId) == null)
                return RedirectToAction("SignIn", "Home");
            return View();
        }


        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string nickName)
        {
            var user = _userService.GetUserByNickName(nickName);
            if (user == null)
            {
                user = await _userService.Add(nickName);
                _contextAccessor.HttpContext.Response.Cookies.Append("rtca-uid", user.Id);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion



        #region Rooms
        [HttpPost]
        public IActionResult _GetAllRooms(string selectedRoomId)
        {
            string userId = (_contextAccessor.HttpContext.Request.Cookies["rtca-uid"] ?? "");
            HomeIndexViewModel model = new();
            model.SelectedRoomId = selectedRoomId;
            model.Rooms = _roomService.GetAll();
            model.UserRooms = _roomUserService.GetUserRooms(userId);
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult _GetRoomMessages(string roomId)
        {
            string userId = (_contextAccessor.HttpContext.Request.Cookies["rtca-uid"] ?? "");

            HomeIndexViewModel model = new();
            model.IsJoinned = _roomUserService.GetUserRooms(userId).Any(x => x.UserId == userId && x.RoomId == roomId);
            model.SelectedRoomId = roomId;
            if (model.IsJoinned)
            {
                model.RoomMessages = _messageService.GetRoomMesssages(roomId);
            }
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult CreateRoom(string roomName)
        {
            string userId = (_contextAccessor.HttpContext.Request.Cookies["rtca-uid"] ?? "");
            _roomService.Add(roomName, userId);
            return Ok("");
        }
        [HttpPost]
        public IActionResult JoinToRoom(string roomId)
        {
            string userId = (_contextAccessor.HttpContext.Request.Cookies["rtca-uid"] ?? "");
            _roomUserService.JoinToRoom(userId, roomId);
            return Ok("");
        }
        #endregion


        #region Message
        [HttpPost]
        public IActionResult SaveMessage(string message, string roomId)
        {
            string userId = (_contextAccessor.HttpContext.Request.Cookies["rtca-uid"] ?? "");
            _messageService.AddMessage(message, userId, roomId);
            return Ok("");
        }
        #endregion

    }
}