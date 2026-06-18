using System.Security.Cryptography;
using FlightStatus.Api.Contracts;

namespace FlightStatus.Api.Providers;

public class QuickFlightProvider : IFlightStatusProvider
{
    public string ProviderName => "QuickFlight";

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
            : (DateTimeOffset?)scheduledDeparture.AddMinutes(random.Next(-20, 60));

        var scheduledArrival = scheduledDeparture.AddHours(random.Next(1, 12));
        var actualArrival = status == UnifiedFlightStatus.Cancelled || status == UnifiedFlightStatus.Diverted
            ? null
            : (DateTimeOffset?)scheduledArrival.AddMinutes(random.Next(-10, 45));

        var gates = new[] { "G1", "G2", "G3", "G4", "G5", "H1", "H2", "H3" };
        var terminals = new[] { "N", "S", "E", "W" };

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
            DelayReason = status == UnifiedFlightStatus.Delayed ? "Mechanical inspection" : null,
            LastUpdatedUtc = DateTimeOffset.UtcNow,
            Provider = ProviderName,
            Message = status switch
            {
                UnifiedFlightStatus.OnTime => "Flight on schedule",
                UnifiedFlightStatus.Delayed => "Flight running behind schedule",
                UnifiedFlightStatus.Cancelled => "Flight cancelled",
                UnifiedFlightStatus.Diverted => "Flight diverted to alternate airport",
                _ => "Flight status unavailable"
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
