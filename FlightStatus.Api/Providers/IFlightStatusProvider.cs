using FlightStatus.Api.Contracts;

namespace FlightStatus.Api.Providers;

public interface IFlightStatusProvider
{
    string ProviderName { get; }

    Task<FlightStatusResult?> GetStatusAsync(
        string flightNumber,
        DateOnly date);
}