using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data.Repository
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
    }
}
