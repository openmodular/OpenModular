namespace OpenModular.Module.Web;

public class APIResponse(int code, string message)
{
    /// <summary>
    /// 错误码
    /// </summary>
    public int Code { get; } = code;

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; } = message;

    public static APIResponse Success()
    {
        return new APIResponse(0, "success");
    }

    public static APIResponse<TData> Success<TData>(TData data)
    {
        return new APIResponse<TData>(0, "success", data);
    }
}

public class APIResponse<TData>(int code, string message, TData data) : APIResponse(code, message)
{
    public TData Data { get; } = data;
}