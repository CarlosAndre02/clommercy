# Main Topics learned

## Sql Server
...

## AdoNet
...

## Repository, Unit of Work and Data Mapper patterns
...

## Clean Architecture
Following the principle of Separation of Concerns, the project aims to decouple business logic from other concerns of the application.
In this way, it proposes a system that is easier to test, independent of frameworks, databases, or any other external agents.

The architecture is divided into layers to make the dependency relationships within the system clear. Outer layers always depend on
inner layers, never the opposite. Instead of a layer depending on the implementation of an external agent, it can depend directly on
an abstraction, represented by a data structure or interface. The external agent then implements the abstraction, creating a contract
that brings stability to the inner layer.

The application layers are defined as (inner to outer):
- Domain Layer – Entities (Clommercy.Core)
- Application Layer – Use Cases (Clommercy.Core)
- Adapter Layer – Repository and Unit of Work (Clommercy.Persistence)
- Infra Layer – AdoNet/Sql Server (Clommercy.Persistence); Routes (Clommercy.WebApi); Controllers (Clommercy.WebApi)

## Dependency Injection
Makes you develop against abstractions, not implementations. Defines a interface between two layers, and everything the inner layer knows
about the dependency comes from the interface. This approach allows code to be more testable and flexible.

## Inversion of Control
It shifts the responsibility of injecting the dependency to someone else. It can be done by a container, which controls the lifecycle of
the dependencies and inject them where necessary.

## Domain-Driven Design
...