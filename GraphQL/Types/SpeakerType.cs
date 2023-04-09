using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Sessions;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Types
{
    public class SpeakerType : ObjectType<Speaker>
    {
        protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ISpeakerByIdDataLoader>()
                .LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.SessionSpeakers)
                .ResolveWith<SpeakerResolvers>(t => SpeakerResolvers.GetSessionsAsync(default!, default!, default!, default))
                .Name("sessions");
        }

        private class SpeakerResolvers
        {
            public static async Task<IEnumerable<Session>> GetSessionsAsync(
                [Parent]Speaker speaker,
                [Service(ServiceKind.Resolver)] ISpeakerRepository speakerRepository,
                ISessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                var sessionIds = await speakerRepository.GetSpeakerSessionsIdsAsync(speaker.Id, cancellationToken);

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}