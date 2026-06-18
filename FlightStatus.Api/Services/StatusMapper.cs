namespace FlightStatus.Api.Services;

using FlightStatus.Api.Contracts;

public static class StatusMapper
{
    public static UnifiedFlightStatus MapAeroTrackStatus(string status)
    {
        return status switch
        {
            "Scheduled" => UnifiedFlightStatus.OnTime,
            "Delayed" => UnifiedFlightStatus.Delayed,
            "Cancelled" => UnifiedFlightStatus.Cancelled,
            "Diverted" => UnifiedFlightStatus.Diverted,
            _ => UnifiedFlightStatus.Unknown
        };
    }

    public static UnifiedFlightStatus MapQuickFlightStatus(string status)
    {
        return status switch
        {
            "ON_SCHEDULE" => UnifiedFlightStatus.OnTime,
            "LATE" => UnifiedFlightStatus.Delayed,
            "CANCELLED" => UnifiedFlightStatus.Cancelled,
            "REDIRECTED" => UnifiedFlightStatus.Diverted,
            _ => UnifiedFlightStatus.Unknown
        };
    }
}