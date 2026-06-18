\# Flight Status Lookup Specification



\## Overview



The Flight Status Lookup feature allows a support agent to search for a flight by flight number and date.



The system retrieves flight status information from multiple flight data providers, normalizes provider-specific responses into a unified model, and returns a single authoritative result.



\---



\## Assumptions



1\. Flight number is required.

2\. Flight date is required.

3\. All provider timestamps are stored in UTC.

4\. Stub providers return deterministic responses using hardcoded data.

5\. When multiple providers return data, the response with the latest `LastUpdatedUtc` timestamp is selected.

6\. Provider failures should not fail the entire request if another provider returns a valid response.

7\. The application does not persist data and therefore does not require a database.

8\. Flight status information is retrieved on demand.



\---



\## Unified Flight Status



The system will normalize provider-specific statuses into the following unified statuses:



| Unified Status | Description                                                 |

| -------------- | ----------------------------------------------------------- |

| OnTime         | Flight departed or arrived within 15 minutes of schedule    |

| Delayed        | Flight departure or arrival delayed by more than 15 minutes |

| Cancelled      | Flight has been cancelled                                   |

| Diverted       | Flight landed at a different airport                        |

| Unknown        | No usable status available                                  |



\---



\## Provider Status Mapping



\### AeroTrack



| Provider Status | Unified Status |

| --------------- | -------------- |

| Scheduled       | OnTime         |

| Delayed         | Delayed        |

| Cancelled       | Cancelled      |

| Diverted        | Diverted       |

| Any Other Value | Unknown        |



\### QuickFlight



| Provider Status | Unified Status |

| --------------- | -------------- |

| ON\_SCHEDULE     | OnTime         |

| LATE            | Delayed        |

| CANCELLED       | Cancelled      |

| REDIRECTED      | Diverted       |

| Any Other Value | Unknown        |



\---



\## Architecture



Client

→ Flight Status API

→ FlightStatusAggregator

→ IFlightStatusProvider



Implementations:



\* AeroTrackProvider

\* QuickFlightProvider



The API endpoint depends only on abstractions and does not reference provider implementations directly.



\---



\## API Endpoint



GET /flights/status?flightNumber={code}\&date={yyyy-MM-dd}



\### Validation Rules



\* Flight number is required

\* Date is required

\* Invalid requests return HTTP 400



\---



\## Data Model



\### FlightStatusResult



| Property              | Type                |

| --------------------- | ------------------- |

| FlightNumber          | string              |

| Status                | UnifiedFlightStatus |

| ScheduledDepartureUtc | DateTime            |

| ActualDepartureUtc    | DateTime?           |

| ScheduledArrivalUtc   | DateTime            |

| ActualArrivalUtc      | DateTime?           |

| Terminal              | string?             |

| Gate                  | string?             |

| DelayReason           | string?             |

| LastUpdatedUtc        | DateTime            |

| Provider              | string              |

| Message               | string?             |



\---



\## Provider Contract



```csharp

public interface IFlightStatusProvider

{

&#x20;   string ProviderName { get; }



&#x20;   Task<FlightStatusResult?> GetStatusAsync(

&#x20;       string flightNumber,

&#x20;       DateOnly date);

}

```



\## Error Handling



\* Provider exceptions are logged.

\* A failure in one provider does not stop execution of other providers.

\* If no provider returns data, the API returns a FlightStatusResult with status `Unknown`.



\## Testing Scope



Unit tests will cover:



1\. Status normalization logic.

2\. Latest provider selection logic.

3\. Provider failure scenarios.

4\. Unknown status scenarios.

5\. Input validation.

6\. Aggregation behavior.



