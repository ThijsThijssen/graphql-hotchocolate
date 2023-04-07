﻿using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;
using System.Collections.Generic;

namespace GraphQL.Data.Repository
{
    public interface ISpeakerRepository
    {
        public Task<IReadOnlyList<Speaker>> GetSpeakersAsync(CancellationToken cancellationToken = default);
        public Task<IReadOnlyDictionary<int, Speaker>> GetSpeakersByIdAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken = default);
        public Task<int[]> GetSpeakerSessionsByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<AddSpeakerPayload> AddSpeakerAsync(AddSpeakerInput input, CancellationToken cancellationToken = default);
    }
}
