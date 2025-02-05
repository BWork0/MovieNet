namespace MovieNet.Application.DTOs
{
    public class EditUserProfileDto
    {
        public Guid UserGuid { get; set; }
        public string? NewUserName { get; set; }
        public DateOnly? NewBirthday { get; set; }
    }
}
