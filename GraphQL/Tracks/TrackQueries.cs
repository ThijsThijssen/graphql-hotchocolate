using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.Tracks
{
    [QueryType]
    public class TrackQueries
    {
        public async Task<IEnumerable<Track>> GetTracksAsync(
            [Service(ServiceKind.Resolver)] ITrackRepository trackRepository,
            CancellationToken cancellationToken) =>
            await trackRepository.GetTracksAsync(cancellationToken);

        public async Task<Track> GetTrackByNameAsync(
            string name,
            [Service(ServiceKind.Resolver)] ITrackRepository trackRepository,
            CancellationToken cancellationToken) =>
            await trackRepository.GetTracksByNameAsync(name, cancellationToken);

        public async Task<IEnumerable<Track>> GetTrackByNamesAsync(
            string[] names,
            [Service(ServiceKind.Resolver)] ITrackRepository trackRepository,
            CancellationToken cancellationToken) =>
            await trackRepository.GetTrackByNamesAsync(names, cancellationToken);

        public Task<Track> GetTrackByIdAsync(
            [ID(nameof(Track))] int id,
            ITrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            trackById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Track>> GetTracksByIdAsync(
            [ID(nameof(Track))] int[] ids,
            ITrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            await trackById.LoadAsync(ids, cancellationToken);

        [DataLoader]
        internal static async Task<IReadOnlyDictionary<int, Track>> GetTrackByIdAsync(
            IReadOnlyList<int> keys,
            ITrackRepository trackRepository,
            CancellationToken cancellationToken)
            => await trackRepository.GetTracksByIdAsync(keys, cancellationToken);
    }
}