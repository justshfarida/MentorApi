﻿namespace MentorApi.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName     { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
