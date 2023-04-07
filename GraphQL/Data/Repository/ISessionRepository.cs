using ConferencePlanner.GraphQL.Data;

namespace GraphQL.Data.Repository
{
    public interface ISessionRepository
    {
        public Task<IReadOnlyDictionary<int, Session>> GetSessionsByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken);
    }
}
