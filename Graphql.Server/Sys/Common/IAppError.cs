using Serilog;

namespace Graphql.Server.Sys.Common;

[GraphQLName("AppError")]
public class AppError : Exception
{
    public AppError() : this(null, null)
    {
    }

    public AppError(string? message, Exception? innerException) : base(message, innerException)
    {
        Code = ErrorEnum.Undefined;
        Message = message ?? "500";
    }

    public AppError(ErrorEnum code, string message)
    {
        Code = code;
        Message = message;
    }


    private ErrorEnum Code { get; }
    private string Message { get; }

    /// <summary>
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    public static IError ErrorFilter(IError arg)
    {
        if (arg.Exception is AppError) return arg;

        Log.Logger.Error(arg.Exception, arg.Message);
        return new Error("service inner error", ErrorEnum.Undefined.ToString("D"),
            exception: new AppError(ErrorEnum.Undefined, "service inner error"));
    }
}
