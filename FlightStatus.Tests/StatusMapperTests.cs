using FlightStatus.Api.Contracts;
using FlightStatus.Api.Services;

namespace FlightStatus.Tests;

public class StatusMapperTests
{
    [Fact]
    public void MapAeroTrack_Delayed_ReturnsDelayed()
    {
        var result = StatusMapper.MapAeroTrackStatus("Delayed");

        Assert.Equal(
            UnifiedFlightStatus.Delayed,
            result);
    }

    [Fact]
    public void MapAeroTrack_Unknown_ReturnsUnknown()
    {
        var result = StatusMapper.MapAeroTrackStatus("SomethingElse");

        Assert.Equal(
            UnifiedFlightStatus.Unknown,
            result);
    }

    [Fact]
    public void MapQuickFlight_LATE_ReturnsDelayed()
    {
        var result = StatusMapper.MapQuickFlightStatus("LATE");

        Assert.Equal(
            UnifiedFlightStatus.Delayed,
            result);
    }

    [Fact]
    public void MapQuickFlight_CANCELLED_ReturnsCancelled()
    {
        var result = StatusMapper.MapQuickFlightStatus("CANCELLED");

        Assert.Equal(
            UnifiedFlightStatus.Cancelled,
            result);
    }
}