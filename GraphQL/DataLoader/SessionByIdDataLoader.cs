using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;

namespace GraphQL.DataLoader
{
    public class SessionByIdDataLoader : BatchDataLoader<int, Session>
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionByIdDataLoader(
            IBatchScheduler batchScheduler, 
            ISessionRepository sessionRepository) 
            : base(batchScheduler)
        {
            _sessionRepository = sessionRepository
                ?? throw new ArgumentNullException(nameof(sessionRepository));
        }

        protected async override Task<IReadOnlyDictionary<int, Session>> LoadBatchAsync(
            IReadOnlyList<int> keys, 
            CancellationToken cancellationToken)
        {
            return await _sessionRepository.GetSessionsByIdAsync(keys, cancellationToken);
        }
    }
}
