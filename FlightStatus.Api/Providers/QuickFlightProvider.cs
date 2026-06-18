using FlightStatus.Api.Contracts;

namespace FlightStatus.Api.Providers;

public class QuickFlightProvider : IFlightStatusProvider
{
    public string ProviderName => "QuickFlight";

    public Task<FlightStatusResult?> GetStatusAsync(
        string flightNumber,
        DateOnly date)
    {
        FlightStatusResult? result = flightNumber.ToUpper() switch
        {
            "AI101" => new FlightStatusResult
            {
                FlightNumber = "AI101",
                Status = UnifiedFlightStatus.OnTime,
                ScheduledDepartureUtc = new DateTime(2025, 06, 18, 08, 00, 00, DateTimeKind.Utc),
                ScheduledArrivalUtc = new DateTime(2025, 06, 18, 10, 00, 00, DateTimeKind.Utc),
                LastUpdatedUtc = DateTime.UtcNow.AddMinutes(-10),
                Provider = ProviderName,
                Message = "Flight on schedule"
            },

            "EK501" => new FlightStatusResult
            {
                FlightNumber = "EK501",
                Status = UnifiedFlightStatus.Cancelled,
                ScheduledDepartureUtc = new DateTime(2025, 06, 18, 15, 00, 00, DateTimeKind.Utc),
                ScheduledArrivalUtc = new DateTime(2025, 06, 18, 19, 00, 00, DateTimeKind.Utc),
                LastUpdatedUtc = DateTime.UtcNow.AddMinutes(-10),
                Provider = ProviderName,
                Message = "Flight cancelled"
            },

            _ => null
        };

        return Task.FromResult(result);
    }
}