namespace MentorApi.DTOs.TokenDTOs
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime ExpirationTime { get; set; }//lifetime
        public string RefreshToken { get; set; }

    }
}
