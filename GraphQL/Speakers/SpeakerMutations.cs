using ConferencePlanner.GraphQL.Speakers;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL
{
    [MutationType]
    public class SpeakerMutations
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