using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Speakers
{
    [QueryType]
    public class SpeakerQueries
    {
        public Task<IReadOnlyList<Speaker>> GetSpeakers(
            ISpeakerRepository speakerRepository,
            CancellationToken cancellationToken)
                => speakerRepository.GetSpeakersAsync(cancellationToken);

        public Task<Speaker> GetSpeakerByIdAsync(
            [ID(nameof(Speaker))] int id,
            ISpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))] int[] ids,
            ISpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);

        [DataLoader]
        internal static async Task<IReadOnlyDictionary<int, Speaker>> GetSpeakerByIdAsync(
            IReadOnlyList<int> keys,
            ISpeakerRepository speakerRepository,
            CancellationToken cancellationToken) 
            => await speakerRepository.GetSpeakersByIdAsync(keys, cancellationToken);
    }
}