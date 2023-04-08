using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using HotChocolate;
using GraphQL.DataLoader;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Sessions
{
    public class ScheduleSessionPayload : SessionPayloadBase
    {
        public ScheduleSessionPayload(Session session)
            : base(session)
        {
        }

        public ScheduleSessionPayload(UserError error)
            : base(new[] { error })
        {
        }

        public async Task<Track?> GetTrackAsync(
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken)
        {
            if (Session is null)
            {
                return null;
            }

            return await trackById.LoadAsync(Session.Id, cancellationToken);
        }

        public async Task<IEnumerable<Speaker>?> GetSpeakersAsync(
            [Service(ServiceKind.Resolver)] ISessionRepository sessionRepository,
            SpeakerByIdDataLoader speakerById,
            CancellationToken cancellationToken)
        {
            if (Session is null)
            {
                return null;
            }

            int[] speakerIds = await sessionRepository.GetSessionSpeakerIdsAsync(Session.Id, cancellationToken);

            return await speakerById.LoadAsync(speakerIds, cancellationToken);
        }
    }
}