using System.Threading;
using System.Threading.Tasks;
using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;
using HotChocolate;
using HotChocolate.Types;

namespace ConferencePlanner.GraphQL.Tracks
{
    [MutationType]
    public class TrackMutations
    {
        public async Task<AddTrackPayload> AddTrackAsync(
            AddTrackInput input,
            [Service(ServiceKind.Resolver)] ITrackRepository trackRepository,
            CancellationToken cancellationToken)
        {
            return await trackRepository.AddTrackAsync(input, cancellationToken);
        }

        public async Task<RenameTrackPayload> RenameTrackAsync(
            RenameTrackInput input,
            [Service(ServiceKind.Resolver)] ITrackRepository trackRepository,
            CancellationToken cancellationToken)
        {
            return await trackRepository.RenameTrackAsync(input, cancellationToken);
        }
    }
}