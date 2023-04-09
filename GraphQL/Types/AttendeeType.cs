using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Sessions;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Types
{
    public class AttendeeType : ObjectType<Attendee>
    {
        protected override void Configure(IObjectTypeDescriptor<Attendee> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<IAttendeeByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.SessionsAttendees)
                .ResolveWith<AttendeeResolvers>(t => AttendeeResolvers.GetSessionsAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("sessions");
        }

        private class AttendeeResolvers
        {
            public static async Task<IEnumerable<Session>> GetSessionsAsync(
                [Parent]Attendee attendee,
                [Service(ServiceKind.Resolver)] IAttendeeRepository attendeeRepository,
                ISessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] sessionIds = await attendeeRepository.GetAttendeeSessionsIdsAsync(attendee.Id, cancellationToken);

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}