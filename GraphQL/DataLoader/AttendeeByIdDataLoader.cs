using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;

namespace ConferencePlanner.GraphQL.DataLoader
{
    public class AttendeeByIdDataLoader : BatchDataLoader<int, Attendee>
    {
        private readonly IAttendeeRepository _attendeeRepository;

        public AttendeeByIdDataLoader(
            IBatchScheduler batchScheduler,
            IAttendeeRepository attendeeRepository)
            : base(batchScheduler)
        {
            _attendeeRepository = attendeeRepository
                ?? throw new ArgumentNullException(nameof(attendeeRepository));
        }

        protected override async Task<IReadOnlyDictionary<int, Attendee>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            return await _attendeeRepository.GetAttendeesByIdAsync(keys, cancellationToken);
        }
    }
}