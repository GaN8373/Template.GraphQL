namespace Graphql.Server.Module.Book;

[QueryType]
public static class QueryBaseBook
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Datasource.Database.Book> GetBooks()
    {
        return new List<Datasource.Database.Book>
        {
            new() { Name = "Book1" }, new()
            {
                Name = "Book2"
            }
        }.AsQueryable();
    }
}