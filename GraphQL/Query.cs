using ConferencePlanner.GraphQL.Data;
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
    }
}