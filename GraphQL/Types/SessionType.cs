using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Sessions;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Types
{
    public class SessionType : ObjectType<Session>
    {
        protected override void Configure(IObjectTypeDescriptor<Session> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ISessionByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.SessionSpeakers)
                .ResolveWith<SessionResolvers>(t => SessionResolvers.GetSpeakersAsync(default!, default!, default!, default))
                .Name("speakers");

            descriptor
                .Field(t => t.SessionAttendees)
                .ResolveWith<SessionResolvers>(t => SessionResolvers.GetAttendeesAsync(default!, default!, default!, default))
                .Name("attendees");

            descriptor
                .Field(t => t.Track)
                .ResolveWith<SessionResolvers>(t => SessionResolvers.GetTrackAsync(default!, default!, default));

            descriptor
                .Field(t => t.TrackId)
                .ID(nameof(Track));
        }

        private class SessionResolvers
        {
            public static async Task<IEnumerable<Speaker>> GetSpeakersAsync(
                [Parent]Session session,
                [Service(ServiceKind.Resolver)] ISessionRepository sessionRepository,
                ISpeakerByIdDataLoader speakerById,
                CancellationToken cancellationToken)
            {
                int[] speakerIds = await sessionRepository.GetSessionSpeakerIdsAsync(session.Id, cancellationToken);

                return await speakerById.LoadAsync(speakerIds, cancellationToken);
            }

            public static async Task<IEnumerable<Attendee>> GetAttendeesAsync(
                [Parent]Session session,
                [Service(ServiceKind.Resolver)] ISessionRepository sessionRepository,
                IAttendeeByIdDataLoader attendeeById,
                CancellationToken cancellationToken)
            {
                int[] attendeeIds = await sessionRepository.GetSessionAttendeeIdsAsync(session.Id, cancellationToken);

                return await attendeeById.LoadAsync(attendeeIds, cancellationToken);
            }

            public static async Task<Track?> GetTrackAsync(
                [Parent]Session session,
                ITrackByIdDataLoader trackById,
                CancellationToken cancellationToken)
            {
                if (session.TrackId is null)
                {
                    return null;
                }

                return await trackById.LoadAsync(session.TrackId.Value, cancellationToken);
            }
        }
    }
}