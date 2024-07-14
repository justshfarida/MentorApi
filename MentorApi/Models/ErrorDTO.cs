using Microsoft.AspNetCore.Components.Forms;

namespace MentorApi.Models
{
    public class ErrorDTO
    {
        public List<string>? Errors { get;private set; }= new List<string>();
        public ErrorDTO(string dto)
        {
            Errors.Add(dto);
        }
        public ErrorDTO(List<string> errors)
        {
            Errors = errors;   
        }
        public ErrorDTO() 
        { 
        }

    }
}
