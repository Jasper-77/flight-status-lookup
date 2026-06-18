using FlightStatus.Api.Contracts;

namespace FlightStatus.Api.Providers;

public class AeroTrackProvider : IFlightStatusProvider
{
    public string ProviderName => "AeroTrack";

    public Task<FlightStatusResult?> GetStatusAsync(
        string flightNumber,
        DateOnly date)
    {
        FlightStatusResult? result = flightNumber.ToUpper() switch
        {
            "AI101" => new FlightStatusResult
            {
                FlightNumber = "AI101",
                Status = UnifiedFlightStatus.Delayed,
                ScheduledDepartureUtc = new DateTime(2025, 06, 18, 08, 00, 00, DateTimeKind.Utc),
                ActualDepartureUtc = new DateTime(2025, 06, 18, 08, 45, 00, DateTimeKind.Utc),
                ScheduledArrivalUtc = new DateTime(2025, 06, 18, 10, 00, 00, DateTimeKind.Utc),
                ActualArrivalUtc = new DateTime(2025, 06, 18, 10, 45, 00, DateTimeKind.Utc),
                Gate = "A12",
                Terminal = "T1",
                DelayReason = "Weather conditions",
                LastUpdatedUtc = DateTime.UtcNow.AddMinutes(-5),
                Provider = ProviderName,
                Message = "Flight delayed due to weather"
            },

            "BA202" => new FlightStatusResult
            {
                FlightNumber = "BA202",
                Status = UnifiedFlightStatus.OnTime,
                ScheduledDepartureUtc = new DateTime(2025, 06, 18, 11, 00, 00, DateTimeKind.Utc),
                ActualDepartureUtc = new DateTime(2025, 06, 18, 11, 05, 00, DateTimeKind.Utc),
                ScheduledArrivalUtc = new DateTime(2025, 06, 18, 14, 00, 00, DateTimeKind.Utc),
                ActualArrivalUtc = new DateTime(2025, 06, 18, 14, 03, 00, DateTimeKind.Utc),
                Gate = "B05",
                Terminal = "T2",
                DelayReason = null,
                LastUpdatedUtc = DateTime.UtcNow.AddMinutes(-5),
                Provider = ProviderName,
                Message = "Flight on time"
            },

            _ => null
        };

        return Task.FromResult(result);
    }
}