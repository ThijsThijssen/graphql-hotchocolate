using ConferencePlanner.GraphQL.Data;
using GraphQL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContextPool<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

builder.Services
    .AddScoped<ISpeakerRepository, SpeakerRepository>()
    .AddScoped<ISessionRepository, SessionRepository>()
    .AddScoped<IAttendeeRepository, AttendeeRepository>()
    .AddScoped<ITrackRepository, TrackRepository>();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddGlobalObjectIdentification()
    .RegisterService<ISpeakerRepository>(ServiceKind.Resolver)
    .RegisterService<ISessionRepository>(ServiceKind.Resolver)
    .RegisterService<IAttendeeRepository>(ServiceKind.Resolver)
    .RegisterService<ITrackRepository>(ServiceKind.Resolver);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();
