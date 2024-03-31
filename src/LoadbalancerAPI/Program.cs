using LoadbalancerAPI.Implementation;
using LoadbalancerAPI.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<StartupService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IHealthCheck, HealthCheck>();
builder.Services.AddSingleton<ILoadbalancer, RoundRobinLogic>();

Log.Logger =
    new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();