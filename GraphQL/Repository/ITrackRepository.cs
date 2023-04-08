using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Tracks;

namespace GraphQL.Repository
{
    public interface ITrackRepository
    {
        public Task<AddTrackPayload> AddTrackAsync(AddTrackInput input, CancellationToken cancellationToken);
        public Task<IEnumerable<Track>> GetTrackByNamesAsync(string[] names, CancellationToken cancellationToken);
        public Task<IEnumerable<Track>> GetTracksAsync(CancellationToken cancellationToken);
        public Task<IReadOnlyDictionary<int, Track>> GetTracksByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken = default);
        public Task<Track> GetTracksByNameAsync(string name, CancellationToken cancellationToken);
        public Task<int[]> GetTrackSessionIdsAsync(int id, CancellationToken cancellationToken = default);
        public Task<RenameTrackPayload> RenameTrackAsync(RenameTrackInput input, CancellationToken cancellationToken);
    }
}
