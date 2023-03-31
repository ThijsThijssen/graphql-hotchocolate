using ConferencePlanner.GraphQL.Speakers;
using GraphQL.Data.Repository;

namespace ConferencePlanner.GraphQL
{
    [MutationType]
    public class Mutation
    {
        public async Task<AddSpeakerPayload> AddSpeakerAsync(
            AddSpeakerInput input,
            ISpeakerRepository speakerRepository,
            CancellationToken cancellationToken)
        {
            return await speakerRepository.AddSpeakerAsync(input, cancellationToken);
        }
    }
}