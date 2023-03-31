using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;

namespace GraphQL.Data.Repository
{
    public interface ISpeakerRepository
    {
        public Task<IReadOnlyList<Speaker>> GetSpeakersAsync(CancellationToken cancellationToken = default);
        public Task<AddSpeakerPayload> AddSpeakerAsync(AddSpeakerInput input, CancellationToken cancellationToken = default);
    }
}
