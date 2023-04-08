using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.DataLoader
{
    public class TrackByIdDataLoader : BatchDataLoader<int, Track>
    {
        private readonly ITrackRepository _trackRepository;

        public TrackByIdDataLoader(
            IBatchScheduler batchScheduler,
            ITrackRepository trackRepository) : base(batchScheduler)
        {
            _trackRepository = trackRepository
                ?? throw new ArgumentNullException(nameof(trackRepository));
        }

        protected override async Task<IReadOnlyDictionary<int, Track>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            return await _trackRepository.GetTracksByIdAsync(keys, cancellationToken);
        }
    }
}
