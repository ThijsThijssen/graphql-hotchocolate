using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Sessions
{
    [MutationType]
    public class SessionMutations
    {
        public async Task<AddSessionPayload> AddSessionAsync(
            AddSessionInput input,
            [Service(ServiceKind.Resolver)] ISessionRepository sessionRepository,
            CancellationToken cancellationToken)
        {
            return await sessionRepository.AddSessionAsync(input, cancellationToken);
        }

        public async Task<ScheduleSessionPayload> ScheduleSessionAsync(
            ScheduleSessionInput input,
            [Service(ServiceKind.Resolver)] ISessionRepository sessionRepository,
            CancellationToken cancellationToken)
        {
            return await sessionRepository.ScheduleSessionAsync(input, cancellationToken);
        }
    }
}