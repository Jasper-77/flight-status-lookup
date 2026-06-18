using FlightStatus.Api.Contracts;
using FlightStatus.Api.Providers;

namespace FlightStatus.Api.Services;

public class FlightStatusAggregator
{
    private readonly IEnumerable<IFlightStatusProvider> _providers;
    private readonly ILogger<FlightStatusAggregator> _logger;

    public FlightStatusAggregator(IEnumerable<IFlightStatusProvider> providers, ILogger<FlightStatusAggregator> logger)
    {
        _providers = providers;
        _logger = logger;
    }

    public async Task<FlightStatusResult> GetFlightStatusAsync(string flightNumber, DateOnly date)
    {
        var tasks = _providers.Select(provider => GetProviderResponse(provider, flightNumber, date));

        var responses = await Task.WhenAll(tasks);

        var validResponses = responses
            .Where(r => r != null)
            .Cast<FlightStatusResult>()
            .ToList();

        if (!validResponses.Any())
        {
            return new FlightStatusResult
            {
                FlightNumber = flightNumber,
                Status = UnifiedFlightStatus.Unknown,
                Provider = "None",
                Message = "No provider returned flight information."
            };
        }

        return validResponses.OrderByDescending(r => r.LastUpdatedUtc).First();
    }

    private async Task<FlightStatusResult?> GetProviderResponse(IFlightStatusProvider provider, string flightNumber, DateOnly date)
    {
        try
        {
            _logger.LogInformation("Calling provider {Provider}", provider.ProviderName);

            return await provider.GetStatusAsync(flightNumber, date);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Provider {Provider} failed", provider.ProviderName);
            return null;
        }
    }
}