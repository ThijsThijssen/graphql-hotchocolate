using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using ConferencePlanner.GraphQL.DataLoader;
using GraphQL.DataLoader;
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
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            [ID(nameof(Session))] int[] ids,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            await sessionById.LoadAsync(ids, cancellationToken);
    }
}