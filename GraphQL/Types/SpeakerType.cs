using ConferencePlanner.GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Data.Repository;

namespace ConferencePlanner.GraphQL.Types
{
    public class SpeakerType : ObjectType<Speaker>
    {
        protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
        {
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
                SessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                var sessionIds = await speakerRepository.GetSpeakerSessionsByIdAsync(speaker.Id, cancellationToken);

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}