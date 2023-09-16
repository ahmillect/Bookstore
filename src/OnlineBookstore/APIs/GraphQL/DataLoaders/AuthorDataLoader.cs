using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.DataLoaders
{
    public class AuthorDataLoader : BatchDataLoader<string, Author>
    {
        private DbContext dbContext { get; }

        public AuthorDataLoader(IBatchScheduler batchScheduler, DbContext context, DataLoaderOptions options = null) : base(batchScheduler, options)
        {
            dbContext = context;
        }

        protected override async Task<IReadOnlyDictionary<string, Author>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var authors = await dbContext.Authors.Find(author => keys.Contains(author.Id)).ToListAsync(cancellationToken);

            return authors.ToDictionary(author => author.Id);
        }
    }
}
