using System.Security.Cryptography;
using FlightStatus.Api.Contracts;

namespace FlightStatus.Api.Providers;

public class AeroTrackProvider : IFlightStatusProvider
{
    public string ProviderName => "AeroTrack";

    public Task<FlightStatusResult?> GetStatusAsync(string flightNumber, DateOnly date)
    {
        // Generate deterministic results based on flight number and date
        var hash = GenerateHash(flightNumber, date);
        var random = new Random(hash);

        var statusValues = Enum.GetValues<UnifiedFlightStatus>();
        var status = statusValues[random.Next(statusValues.Length)];

        // Deterministic times within a day
        var baseTime = date.ToDateTime(TimeOnly.MinValue);
        var scheduledDeparture = new DateTimeOffset(baseTime.AddHours(random.Next(6, 22)), TimeSpan.Zero);
        var actualDeparture = status == UnifiedFlightStatus.Cancelled
            ? null
            : (DateTimeOffset?)scheduledDeparture.AddMinutes(random.Next(-30, 120));

        var scheduledArrival = scheduledDeparture.AddHours(random.Next(1, 12));
        var actualArrival = status == UnifiedFlightStatus.Cancelled || status == UnifiedFlightStatus.Diverted
            ? null
            : (DateTimeOffset?)scheduledArrival.AddMinutes(random.Next(-15, 90));

        var gates = new[] { "A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3" };
        var terminals = new[] { "1", "2", "3" };

        var result = new FlightStatusResult
        {
            FlightNumber = flightNumber,
            Status = status,
            ScheduledDepartureUtc = scheduledDeparture,
            ActualDepartureUtc = actualDeparture,
            ScheduledArrivalUtc = scheduledArrival,
            ActualArrivalUtc = actualArrival,
            Gate = gates[random.Next(gates.Length)],
            Terminal = terminals[random.Next(terminals.Length)],
            DelayReason = status == UnifiedFlightStatus.Delayed ? "Weather delay" : null,
            LastUpdatedUtc = DateTimeOffset.UtcNow,
            Provider = ProviderName,
            Message = status switch
            {
                UnifiedFlightStatus.OnTime => "Flight is on time",
                UnifiedFlightStatus.Delayed => "Flight is delayed",
                UnifiedFlightStatus.Cancelled => "Flight has been cancelled",
                UnifiedFlightStatus.Diverted => "Flight has been diverted",
                _ => "Unknown flight status"
            }
        };

        return Task.FromResult<FlightStatusResult?>(result);
    }

    private static int GenerateHash(string flightNumber, DateOnly date)
    {
        var input = $"{flightNumber}:{date:yyyy-MM-dd}";
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToInt32(hash, 0);
        }
    }
}
