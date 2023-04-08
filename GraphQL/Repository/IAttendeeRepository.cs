using ConferencePlanner.GraphQL.Data;

namespace GraphQL.Repository
{
    public interface IAttendeeRepository
    {
        public Task<IReadOnlyDictionary<int, Attendee>> GetAttendeesByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken = default);
        public Task<int[]> GetAttendeeSessionsIdsAsync(int id, CancellationToken cancellationToken = default);
    }
}
