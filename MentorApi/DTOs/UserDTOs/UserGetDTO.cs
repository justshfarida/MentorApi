namespace MentorApi.DTOs.UserDTOs
{
    public class UserGetDTO
    {
        public string Id { get; set; }
        public string FirtsName { get; set; }
        public string LastName  { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
