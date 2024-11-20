namespace Graphql.Server.Module.Book;

[ExtendObjectType<Datasource.Database.Book>]
public static class ExtendHello
{
    public static string GetExtendHello([Parent] Datasource.Database.Book book)
    {
        return "Hello Extend";
    }
}