using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;
using GraphQL.Repository;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repository
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly ApplicationDbContext _context;

        public SpeakerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Speaker>> GetSpeakersAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Speakers
                .OrderBy(speaker => speaker.Name)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<AddSpeakerPayload> AddSpeakerAsync(
            AddSpeakerInput input,
            CancellationToken cancellationToken = default)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                Bio = input.Bio,
                WebSite = input.WebSite
            };

            _context.Speakers.Add(speaker);
            await _context.SaveChangesAsync(cancellationToken);

            return new AddSpeakerPayload(speaker);
        }

        public async Task<IReadOnlyDictionary<int, Speaker>> GetSpeakersByIdAsync(
            IReadOnlyList<int> keys, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Speakers
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }

        public async Task<int[]> GetSpeakerSessionsIdsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Speakers
                    .Where(s => s.Id == id)
                    .Include(s => s.SessionSpeakers)
                    .SelectMany(s => s.SessionSpeakers.Select(t => t.SessionId))
                    .ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
