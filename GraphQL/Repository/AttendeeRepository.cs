using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repository
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyDictionary<int, Attendee>> GetAttendeesByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken = default)
        {
            return await _context.Attendees
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }

        public async Task<int[]> GetAttendeeSessionsIdsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Attendees
                    .Where(a => a.Id == id)
                    .Include(a => a.SessionsAttendees)
                    .SelectMany(a => a.SessionsAttendees.Select(t => t.SessionId))
                    .ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
