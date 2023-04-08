using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
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
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))] int[] ids,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);
    }
}