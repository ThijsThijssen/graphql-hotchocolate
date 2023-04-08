using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Sessions;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddSessionPayload> AddSessionAsync(AddSessionInput input, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(input.Title))
            {
                return new AddSessionPayload(
                    new UserError("The title cannot be empty.", "TITLE_EMPTY"));
            }

            if (input.SpeakerIds.Count == 0)
            {
                return new AddSessionPayload(
                    new UserError("No speaker assigned.", "NO_SPEAKER"));
            }

            var session = new Session
            {
                Title = input.Title,
                Abstract = input.Abstract,
            };

            foreach (int speakerId in input.SpeakerIds)
            {
                session.SessionSpeakers.Add(new SessionSpeaker
                {
                    SpeakerId = speakerId
                });
            }

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync(cancellationToken);

            return new AddSessionPayload(session);
        }

        public async Task<int[]> GetSessionAttendeeIdsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                    .Where(s => s.Id == id)
                    .Include(session => session.SessionAttendees)
                    .SelectMany(session => session.SessionAttendees.Select(t => t.AttendeeId))
                    .ToArrayAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Session>> GetSessionsAsync(CancellationToken cancellationToken)
        {
            return await _context.Sessions.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyDictionary<int, Session>> GetSessionsByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            return await _context.Sessions
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }

        public async Task<int[]> GetSessionSpeakerIdsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                    .Where(s => s.Id == id)
                    .Include(s => s.SessionSpeakers)
                    .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
                    .ToArrayAsync(cancellationToken: cancellationToken);
        }

        public async Task<ScheduleSessionPayload> ScheduleSessionAsync(ScheduleSessionInput input, CancellationToken cancellationToken = default)
        {
            if (input.EndTime < input.StartTime)
            {
                return new ScheduleSessionPayload(
                    new UserError("endTime has to be larger than startTime.", "END_TIME_INVALID"));
            }

            Session session = await _context.Sessions.FindAsync(new object?[] { input.SessionId }, cancellationToken: cancellationToken);

            if (session is null)
            {
                return new ScheduleSessionPayload(
                    new UserError("Session not found.", "SESSION_NOT_FOUND"));
            }

            session.TrackId = input.TrackId;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;

            await _context.SaveChangesAsync(cancellationToken);

            return new ScheduleSessionPayload(session);
        }
    }
}
