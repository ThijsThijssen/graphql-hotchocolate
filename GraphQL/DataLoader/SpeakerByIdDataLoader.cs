using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.DataLoader
{
    public class SpeakerByIdDataLoader : BatchDataLoader<int, Speaker>
    {
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakerByIdDataLoader(
            IBatchScheduler batchScheduler,
            ISpeakerRepository speakerRepository)
            : base(batchScheduler)
        {
            _speakerRepository = speakerRepository 
                ?? throw new ArgumentNullException(nameof(speakerRepository));
        }

        protected override async Task<IReadOnlyDictionary<int, Speaker>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            return await _speakerRepository.GetSpeakersByIdAsync(keys, cancellationToken);
        }
    }
}