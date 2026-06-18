using FlightStatus.Api.Contracts;
using FlightStatus.Api.Services;
using FlightStatus.Tests.TestDoubles;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlightStatus.Tests.Services;

public class FlightStatusAggregatorTests
{
    [Fact]
    public async Task ReturnsLatestProviderResponse()
    {
        var provider1 = new FakeFlightStatusProvider
        {
            ProviderName = "Provider1",
            Response = new FlightStatusResult
            {
                FlightNumber = "AI101",
                Status = UnifiedFlightStatus.OnTime,
                LastUpdatedUtc = new DateTime(
                    2025, 6, 18, 10, 00, 00,
                    DateTimeKind.Utc)
            }
        };

        var provider2 = new FakeFlightStatusProvider
        {
            ProviderName = "Provider2",
            Response = new FlightStatusResult
            {
                FlightNumber = "AI101",
                Status = UnifiedFlightStatus.Delayed,
                LastUpdatedUtc = new DateTime(
                    2025, 6, 18, 10, 30, 00,
                    DateTimeKind.Utc)
            }
        };

        var aggregator = new FlightStatusAggregator(
            new[] { provider1, provider2 },
            NullLogger<FlightStatusAggregator>.Instance);

        var result = await aggregator.GetFlightStatusAsync(
            "AI101",
            new DateOnly(2025, 6, 18));

        Assert.Equal(
            UnifiedFlightStatus.Delayed,
            result.Status);
    }

    [Fact]
    public async Task ReturnsUnknownWhenNoProviderResponds()
    {
        var provider1 = new FakeFlightStatusProvider();
        var provider2 = new FakeFlightStatusProvider();

        var aggregator = new FlightStatusAggregator(
            new[] { provider1, provider2 },
            NullLogger<FlightStatusAggregator>.Instance);

        var result = await aggregator.GetFlightStatusAsync(
            "XX999",
            new DateOnly(2025, 6, 18));

        Assert.Equal(
            UnifiedFlightStatus.Unknown,
            result.Status);
    }

    [Fact]
    public async Task ReturnsValidResponseWhenOneProviderFails()
    {
        var failingProvider = new FakeFlightStatusProvider
        {
            ThrowException = true
        };

        var validProvider = new FakeFlightStatusProvider
        {
            Response = new FlightStatusResult
            {
                FlightNumber = "AI101",
                Status = UnifiedFlightStatus.OnTime,
                LastUpdatedUtc = DateTime.UtcNow
            }
        };

        var aggregator = new FlightStatusAggregator(
            new[] { failingProvider, validProvider },
            NullLogger<FlightStatusAggregator>.Instance);

        var result = await aggregator.GetFlightStatusAsync(
            "AI101",
            new DateOnly(2025, 6, 18));

        Assert.Equal(
            UnifiedFlightStatus.OnTime,
            result.Status);
    }

    [Fact]
    public async Task ReturnsSingleProviderResponse()
    {
        var provider = new FakeFlightStatusProvider
        {
            Response = new FlightStatusResult
            {
                FlightNumber = "BA202",
                Status = UnifiedFlightStatus.OnTime,
                LastUpdatedUtc = DateTime.UtcNow
            }
        };

        var aggregator = new FlightStatusAggregator(
            new[] { provider },
            NullLogger<FlightStatusAggregator>.Instance);

        var result = await aggregator.GetFlightStatusAsync(
            "BA202",
            new DateOnly(2025, 6, 18));

        Assert.Equal(
            UnifiedFlightStatus.OnTime,
            result.Status);
    }
}