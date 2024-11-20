using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;

namespace Graphql.Server.Sys.Listener;

public class ErrorLogListener(ILogger<ErrorLogListener> logger)
    : ExecutionDiagnosticEventListener
{
    public override void ResolverError(IRequestContext context, ISelection selection, IError error)
    {
        logger.LogError(error.Exception, "ResolverError");
        base.ResolverError(context, selection, error);
    }
}