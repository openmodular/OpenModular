namespace OpenModular.Module.Api;

public class ApiResponse(int code, string message)
{
    /// <summary>
    /// 错误码
    /// </summary>
    public int Code { get; } = code;

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; } = message;

    public static ApiResponse Success()
    {
        return new ApiResponse(0, "success");
    }

    public static ApiResponse<TData> Success<TData>(TData data)
    {
        return new ApiResponse<TData>(0, "success", data);
    }
}

public class ApiResponse<TData>(int code, string message, TData data) : ApiResponse(code, message)
{
    public TData Data { get; } = data;
}