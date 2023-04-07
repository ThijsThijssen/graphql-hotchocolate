using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyDictionary<int, Session>> GetSessionsByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            return await _context.Sessions
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
