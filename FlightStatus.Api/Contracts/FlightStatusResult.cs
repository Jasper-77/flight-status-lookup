namespace FlightStatus.Api.Contracts;

public class FlightStatusResult
{
    public string? Airline { get; set; }

    public string? FlightNumber { get; set; }

    public string? DepartureAirport { get; set; }

    public string? ArrivalAirport { get; set; }

    public DateTimeOffset? ScheduledDepartureUtc { get; set; }

    public DateTimeOffset? ActualDepartureUtc { get; set; }

    public DateTimeOffset? ScheduledArrivalUtc { get; set; }

    public DateTimeOffset? ActualArrivalUtc { get; set; }

    public UnifiedFlightStatus Status { get; set; } = UnifiedFlightStatus.Unknown;

    public string? Gate { get; set; }

    public string? Terminal { get; set; }

    public string? DelayReason { get; set; }

    public DateTimeOffset? LastUpdatedUtc { get; set; }

    public string? Provider { get; set; }

    public string? Message { get; set; }
}
