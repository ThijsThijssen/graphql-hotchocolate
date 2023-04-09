using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Sessions
{
    [QueryType]
    public class SessionQueries
    {
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [Service(ServiceKind.Resolver)] ISessionRepository sessionRepository,
            CancellationToken cancellationToken) =>
            await sessionRepository.GetSessionsAsync(cancellationToken);

        public Task<Session> GetSessionByIdAsync(
            [ID(nameof(Session))] int id,
            ISessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            [ID(nameof(Session))] int[] ids,
            ISessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            await sessionById.LoadAsync(ids, cancellationToken);

        [DataLoader]
        internal static async Task<IReadOnlyDictionary<int, Session>> GetSessionByIdAsync(
           IReadOnlyList<int> keys,
           ISessionRepository sessionRepository,
           CancellationToken cancellationToken)
           => await sessionRepository.GetSessionsByIdAsync(keys, cancellationToken);

        [DataLoader]
        internal static async Task<IReadOnlyDictionary<int, Attendee>> GetAttendeeByIdAsync(
            IReadOnlyList<int> keys,
            IAttendeeRepository attendeeRepository,
            CancellationToken cancellationToken)
            => await attendeeRepository.GetAttendeesByIdAsync(keys, cancellationToken);
    }
}