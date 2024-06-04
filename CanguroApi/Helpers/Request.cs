namespace CanguroApi.Helpers
{
    public class Request<T>
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public Request()
        {
        }

        public Request(bool isOK, string message, T result)
        {
            IsOk = isOK;
            Message = message;
            Result = result;
        }

        public static Request<T> Succes(T result)
        {
            return new Request<T>(isOK : true, message: "", result);

        }

        public static Request<T> NoSucces(string message)
        {
            return new Request<T> (isOK: false, message, default(T));
        }

        public static Request<T> Error(string message)
        {
            return new Request<T>(isOK: false, message, default(T));
        }

    }
}
