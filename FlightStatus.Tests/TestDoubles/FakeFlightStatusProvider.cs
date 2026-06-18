using FlightStatus.Api.Contracts;
using FlightStatus.Api.Providers;

namespace FlightStatus.Tests.TestDoubles;

public class FakeFlightStatusProvider : IFlightStatusProvider
{
    public string ProviderName { get; set; } = string.Empty;

    public FlightStatusResult? Response { get; set; }

    public bool ThrowException { get; set; }

    public Task<FlightStatusResult?> GetStatusAsync(
        string flightNumber,
        DateOnly date)
    {
        if (ThrowException)
        {
            throw new Exception("Provider failed");
        }

        return Task.FromResult(Response);
    }
}