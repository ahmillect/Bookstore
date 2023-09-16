using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.DataLoaders
{
    public class BooksDataLoader : GroupedDataLoader<string, Book>
    {
        private DbContext dbContext { get; }

        public BooksDataLoader(IBatchScheduler batchScheduler, DbContext context, DataLoaderOptions options = null) : base(batchScheduler, options)
        {
            dbContext = context;
        }

        protected override async Task<ILookup<string, Book>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var books = await dbContext.Books.Find(book => keys.Contains(book.AuthorId)).ToListAsync(cancellationToken);

            return books.ToLookup(book => book.AuthorId);
        }
    }
}
