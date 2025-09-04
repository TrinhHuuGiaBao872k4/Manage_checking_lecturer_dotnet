namespace MongoElearn.Api.Common;

public class HTTPResponseClient<T>
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "";
    public DateTime? DateTime { get; set; } = System.DateTime.UtcNow;
    public T? Data { get; set; }

    public static HTTPResponseClient<T> Ok(T? data, string? message = null, int statusCode = 200)
        => new() { StatusCode = statusCode, Message = message ?? "Success", Data = data };

    public static HTTPResponseClient<T> Fail(string message, int statusCode = 400)
        => new() { StatusCode = statusCode, Message = message, Data = default };
}
