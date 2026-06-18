\# Flight Status Lookup



\## Overview



Flight Status Lookup is a full-stack application that aggregates flight status information from multiple providers and presents a unified view to users.



The solution consists of:



\* ASP.NET Core 8 Minimal API

\* Angular 22 Frontend

\* xUnit Unit Tests

\* Provider abstraction and aggregation layer



\## Architecture



\### Backend



\* Minimal API endpoint: `/flights/status`

\* Provider abstraction using `IFlightStatusProvider`

\* Multiple provider implementations:



&#x20; \* AeroTrackProvider

&#x20; \* QuickFlightProvider

\* Status normalization using `StatusMapper`

\* Aggregation service selects the most recently updated provider response



\### Frontend



\* Angular 22 standalone application

\* Angular Material UI

\* Reactive Forms

\* Date Picker support

\* Error and loading states



\## Assumptions



\* Providers are simulated using stub data.

\* Flight Number and Date uniquely identify a flight.

\* Most recently updated provider response is considered authoritative.



\## Running the Solution



\### Backend



```bash

dotnet restore

dotnet build

dotnet run --project FlightStatus.Api

```



\### Frontend



```bash

cd flight-status-ui

npm install

ng serve

```



\### Tests



```bash

dotnet test

```



\## Project Structure



FlightStatus.Api - Backend API



FlightStatus.Tests - Unit Tests



flight-status-ui - Angular Frontend



spec.md - Design and architecture notes



prompts.md - Copilot prompts used during development



reflection.md - Project reflection and learnings



\## GitHub Copilot Usage



GitHub Copilot was used to assist with:



\* Generating initial model and interface scaffolding

\* Provider implementation templates

\* Unit test boilerplate

\* Angular service and component scaffolding

\* Documentation drafts



All generated code was reviewed, validated, and modified before inclusion.



\## Future Improvements



\* Real provider integrations

\* Retry and resilience policies

\* Response caching

\* Docker support

\* CI/CD pipeline

\* OpenTelemetry monitoring



