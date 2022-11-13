namespace MessagingApp.UI.Models.DTOs
{
    public class RoomDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<RoomDetailMessageeDto> Messages { get; set; }
    }
}
