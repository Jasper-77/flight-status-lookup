using FlightStatus.Api.Providers;
using FlightStatus.Api.Services;
using FlightStatus.Api.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddSingleton<IFlightStatusProvider, AeroTrackProvider>();
builder.Services.AddSingleton<IFlightStatusProvider, QuickFlightProvider>();
builder.Services.AddScoped<FlightStatusAggregator>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!");
app.MapFlightStatusEndpoints();
app.UseCors("Angular");
app.Run();
