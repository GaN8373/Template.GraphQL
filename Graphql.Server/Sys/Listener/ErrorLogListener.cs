using HotChocolate.Execution.Instrumentation;
using HotChocolate.Resolvers;

namespace Graphql.Server.Sys.Listener;

public class ErrorLogListener(ILogger<ErrorLogListener> logger)
    : ExecutionDiagnosticEventListener
{
    public override void ResolverError(IMiddlewareContext context, IError error)
    {
        logger.LogError(error.Exception, "ResolverError");
        base.ResolverError(context, error);
    }
}