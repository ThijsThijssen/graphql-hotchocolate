using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Sessions;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Types
{
    public class TrackType : ObjectType<Track>
    {
        protected override void Configure(IObjectTypeDescriptor<Track> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<ITrackByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Sessions)
                .ResolveWith<TrackResolvers>(t => TrackResolvers.GetSessionsAsync(default!, default!, default!, default))
                .Name("sessions");
        }

        private class TrackResolvers
        {
            public static async Task<IEnumerable<Session>> GetSessionsAsync(
                [Parent]Track track,
                [Service(ServiceKind.Resolver)] ITrackRepository trackRepository,
                ISessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] sessionIds = await trackRepository.GetTrackSessionIdsAsync(track.Id, cancellationToken);

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}