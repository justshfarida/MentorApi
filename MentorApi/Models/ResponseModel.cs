namespace MentorApi.Model
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }
    }
}
