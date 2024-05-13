using Application;
using SearchAPI.Implementation;
using SearchAPI.Interfaces;
using SearchAPI.Services;
using Serilog;

Configuration.GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabase, Database>();
builder.Services.AddScoped<ISearchLogic, SearchLogic>();
builder.Services.AddScoped<IUpdateSettings, UpdateSettings>();
builder.Services.AddScoped<IDocsReader, DocsReader>();
builder.Services.AddCors();

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
