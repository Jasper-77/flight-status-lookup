using FlightStatus.Api.Services;

namespace FlightStatus.Api.Endpoints;

public static class FlightStatusEndpoints
{
    public static IEndpointRouteBuilder MapFlightStatusEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/flights/status",
            async (
                string? flightNumber,
                DateOnly? date,
                FlightStatusAggregator aggregator) =>
            {
                if (string.IsNullOrWhiteSpace(flightNumber))
                {
                    return Results.BadRequest(
                        new
                        {
                            Message = "Flight number is required."
                        });
                }

                if (date is null)
                {
                    return Results.BadRequest(
                        new
                        {
                            Message = "Date is required."
                        });
                }

                var result = await aggregator.GetFlightStatusAsync(
                    flightNumber,
                    date.Value);

                return Results.Ok(result);
            });

        return endpoints;
    }
}