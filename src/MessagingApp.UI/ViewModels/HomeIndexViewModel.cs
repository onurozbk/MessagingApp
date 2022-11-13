using MessagingApp.UI.Models.DTOs;
using MessagingApp.UI.Models.MongoDbModels;

namespace MessagingApp.UI.ViewModels
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            this.Rooms = new();
            this.UserRooms = new();
            this.RoomMessages = new();
            this.SelectedRoomId = string.Empty;
        }
        public List<Room> Rooms { get; set; }
        public List<RoomUser> UserRooms { get; set; }
        public List<RoomDetailMessageeDto> RoomMessages { get; set; }
        public string SelectedRoomId { get; set; }
        public bool IsJoinned { get; set; }
    }
}
