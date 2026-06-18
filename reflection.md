\# Reflection



\## What Went Well



The provider abstraction allowed flight status sources to be added without changing endpoint logic.



The aggregation layer cleanly separated provider communication from API concerns.



Angular standalone components simplified frontend development and reduced configuration overhead.



\## Challenges



The most challenging aspect was ensuring a consistent normalized status model across multiple providers.



Frontend integration required handling date formatting and Angular Material component configuration.



Angular change detection and UI rendering required additional debugging during development.



\## GitHub Copilot Usage



GitHub Copilot accelerated development by generating initial implementations, boilerplate code, and test scaffolding.



Generated content was reviewed, refined, and adapted to align with the project requirements.



\## What I Would Improve With More Time



\* Integrate real flight data providers

\* Add retry and circuit breaker policies

\* Add distributed caching

\* Add Docker support

\* Add CI/CD pipeline

\* Improve UI responsiveness and accessibility

\* Add integration and end-to-end tests
