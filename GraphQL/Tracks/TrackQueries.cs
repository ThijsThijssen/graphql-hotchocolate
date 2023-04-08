using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using GraphQL.DataLoader;
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
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            trackById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Track>> GetTracksByIdAsync(
            [ID(nameof(Track))] int[] ids,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            await trackById.LoadAsync(ids, cancellationToken);
    }
}