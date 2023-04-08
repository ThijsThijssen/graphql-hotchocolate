using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Tracks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraphQL.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ApplicationDbContext _context;

        public TrackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddTrackPayload> AddTrackAsync(AddTrackInput input, CancellationToken cancellationToken)
        {
            var track = new Track { Name = input.Name };
            _context.Tracks.Add(track);

            await _context.SaveChangesAsync(cancellationToken);

            return new AddTrackPayload(track);
        }

        public async Task<IEnumerable<Track>> GetTrackByNamesAsync(string[] names, CancellationToken cancellationToken)
        {
            return await _context.Tracks.Where(t => names.Contains(t.Name)).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(CancellationToken cancellationToken)
        {
            return await _context.Tracks.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyDictionary<int, Track>> GetTracksByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken = default)
        {
            return await _context.Tracks
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }

        public Task<Track> GetTracksByNameAsync(string name, CancellationToken cancellationToken)
        {
            return _context.Tracks.FirstAsync(t => t.Name == name, cancellationToken: cancellationToken);
        }

        public async Task<int[]> GetTrackSessionIdsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                    .Where(s => s.Id == id)
                    .Select(s => s.Id)
                    .ToArrayAsync(cancellationToken: cancellationToken);
        }

        public async Task<RenameTrackPayload> RenameTrackAsync(RenameTrackInput input, CancellationToken cancellationToken)
        {
            Track track = await _context.Tracks.FindAsync(new object?[] { input.Id }, cancellationToken: cancellationToken);
            track.Name = input.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return new RenameTrackPayload(track);
        }
    }
}
