using ConferencePlanner.GraphQL.Data;
using GraphQL.Data.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContextPool<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

builder.Services
    .AddScoped<ISpeakerRepository, SpeakerRepository>()
    .AddScoped<ISessionRepository, SessionRepository>();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .RegisterService<ISpeakerRepository>(ServiceKind.Resolver)
    .RegisterService<ISessionRepository>(ServiceKind.Resolver);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();
