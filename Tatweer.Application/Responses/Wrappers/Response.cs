using System.Collections.Generic;

namespace  Tatweer.Application.Responses.Wrappers
{
    public class Response : Response<object>
    {
        public Response(object data, bool succeeded = true, string message = null) : base(data, succeeded, message) { }
        public Response(bool succeeded) : base(succeeded) { }

        public static implicit operator Response(bool successded)
        {
            return new Response(successded);
        }

        public static implicit operator Response(string message)
        {
            return new Response(null, false, message);
        }
    }

    public class Response<T> where T : class
    {
        public Response() { }

        public Response(T data, bool succeeded = true, string message = null)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(bool succeeded)
        {
            Succeeded = succeeded;
            Message = Succeeded ? "Operation has beed completed successfully." : "Operation has been failed!";
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public static implicit operator Response<T>(T data)
        {
            return new Response<T>(data);
        }

        public static implicit operator Response<T>(bool successded)
        {
            return new Response<T>(successded);
        }
    }
}
