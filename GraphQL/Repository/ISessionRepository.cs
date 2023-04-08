using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Sessions;

namespace GraphQL.Repository
{
    public interface ISessionRepository
    {
        public Task<IReadOnlyDictionary<int, Session>> GetSessionsByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken);
        public Task<int[]> GetSessionAttendeeIdsAsync(int id, CancellationToken cancellationToken = default);
        public Task<int[]> GetSessionSpeakerIdsAsync(int id, CancellationToken cancellationToken = default);
        public Task<AddSessionPayload> AddSessionAsync(AddSessionInput input, CancellationToken cancellationToken = default);
        public Task<ScheduleSessionPayload> ScheduleSessionAsync(ScheduleSessionInput input, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Session>> GetSessionsAsync(CancellationToken cancellationToken);
    }
}
