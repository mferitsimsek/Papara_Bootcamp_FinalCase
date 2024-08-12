namespace Papara.CaptainStore.Domain.DTOs
{
    public class ApiResponseDTO<T> where T : class?
    {
        public ApiResponseDTO(int status, T? data, List<string>? messages)
        {
            this.status = status;
            this.data = data;
            this.messages = messages;
        }

        public ApiResponseDTO()
        {
        }

        public int status { get; set; }
        public T? data { get; set; }
        public List<string>? messages { get; set; }
    }
}
