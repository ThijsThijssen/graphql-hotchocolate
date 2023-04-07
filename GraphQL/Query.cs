using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using GraphQL.Data.Repository;


namespace ConferencePlanner.GraphQL
{
    [QueryType]
    public class Query
    {
        public Task<IReadOnlyList<Speaker>> GetSpeakers(
            ISpeakerRepository speakerRepository,
            CancellationToken cancellationToken)
                => speakerRepository.GetSpeakersAsync(cancellationToken);

        public Task<Speaker> GetSpeakerAsync(
            int id,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) 
                => dataLoader.LoadAsync(id, cancellationToken);
    }
}