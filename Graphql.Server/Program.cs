using FreeSql;
using Graphql.Server.Sys.Listener;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration
        .AddJsonFile("appsettings.json")
        .Build())
    .CreateBootstrapLogger();

builder.Services.AddSerilog()
    .AddSingleton<IFreeSql>(provider =>
    {
        var config = provider.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection")!;

        return new FreeSqlBuilder()
            .UseConnectionString(DataType.PostgreSQL, connectionString)
            .UseMonitorCommand(cmd => Log.Logger.Debug($"Sql：{cmd.CommandText}"))
            .UseAutoSyncStructure(false)
            .Build();
    });

builder.AddGraphQL()
    // find source generator code replace the AddTypes, HotChocolateTypeModule
    .AddTypes()
    .ModifyPagingOptions(options =>
    {
        options.IncludeNodesField = true;
        // options.IncludeTotalCount = true;
        options.DefaultPageSize = 20;
    })
    .AddDiagnosticEventListener<ErrorLogListener>()
    .AddSorting()
    .AddFiltering()
    ;

var app = builder.Build();

app.MapGraphQL()
//     .WithOptions(new GraphQLServerOptions
//     {
//         EnableGetRequests = true,
//         AllowedGetOperations = AllowedGetOperations.Query,
//
// #if DEBUG
//         EnableSchemaRequests = true,
// #else
//     EnableSchemaRequests = false,
// #endif
//
//         Tool =
//         {
//             HttpMethod = DefaultHttpMethod.Post,
// #if DEBUG
//             Enable = true,
// #else
//         Enable = false,
// #endif
//         }
//     })
    ;

app.RunWithGraphQLCommands(args);