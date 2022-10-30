using TwelveFactorApp.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPiCalculator, PiCalculator>();
builder.Services.AddSingleton<IRedisDatabase, RedisDatabase>();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(54321);
    options.ListenAnyIP(43210, configure => configure.UseHttps());
});

WebApplication app = builder.Build();

ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();

IHostApplicationLifetime lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

lifetime.ApplicationStopping.Register(() => { logger.LogInformation("Application is stopping\n Doing Cleanup"); });

lifetime.ApplicationStopped.Register(() => { logger.LogInformation("Application stopped"); });

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();