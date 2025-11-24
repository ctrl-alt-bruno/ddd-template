# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is a Domain-Driven Design (DDD) template project for .NET, implementing a modular monolith architecture with a focus on bounded contexts as separate services. The solution is organized using DDD tactical patterns including Entities, Value Objects, Aggregates, and Repositories.

## Project Structure

The solution follows a vertical slice architecture organized by bounded contexts:

```
src/Services/
├── Core/                      # Shared kernel with DDD building blocks
│   └── Ddd.Core/             # Base classes and interfaces for DDD patterns
├── Catalog/                   # Catalog bounded context (example implementation)
│   ├── Ddd.Catalog.Domain/   # Domain layer (entities, value objects, domain logic)
│   ├── Ddd.Catalog.Data/     # Infrastructure layer (EF Core, repositories)
│   └── Ddd.Catalog.Test/     # Unit tests
└── [Other Services]/          # Placeholder folders for future bounded contexts
    ├── Registration/
    ├── Sales/
    ├── Payments/
    └── Fiscal/
```

## Key Architectural Patterns

### Core DDD Building Blocks (Ddd.Core)

- **Entity**: Base class for all domain entities (src/Services/Core/Ddd.Core/DomainObjects/Entity.cs)
  - Identity-based equality using Guid
  - Overrides `Equals()`, `GetHashCode()`, and comparison operators

- **IAggregateRoot**: Marker interface for aggregate roots (src/Services/Core/Ddd.Core/DomainObjects/IAggregateRoot.cs)
  - Only aggregate roots can have repositories

- **AssertionConcern**: Domain validation framework (src/Services/Core/Ddd.Core/DomainObjects/AssertionConcern.cs)
  - Comprehensive static validation methods for domain invariants
  - Throws `DomainException` on validation failures
  - Categories: equality, nullability, strings, ranges (int/long/double/float/decimal), dates, collections, Guids, booleans
  - Domain-specific validators: email, CPF, CNPJ, URL, phone numbers

- **IRepository<T>**: Generic repository interface for aggregate roots (src/Services/Core/Ddd.Core/Data/IRepository.cs)
  - Enforces repository pattern only for aggregates (`where T : IAggregateRoot`)
  - Exposes `IUnitOfWork` for transaction management

- **IUnitOfWork**: Abstraction for transactional boundaries (src/Services/Core/Ddd.Core/Data/IUnitOfWork.cs)
  - Single method: `Task<bool> Commit()`

### Domain Layer Pattern

Domain entities follow these conventions:
- Private setters for encapsulation
- Constructor-based validation using `AssertionConcern`
- Rich domain behavior through public methods (not just getters/setters)
- Value Objects as owned entities (e.g., `Dimensions`)

**Example**: `Product` (src/Services/Catalog/Ddd.Catalog.Domain/Product.cs)
- Aggregate root with business methods: `Activate()`, `Deactivate()`, `ReduceStock()`, `IncreaseStock()`
- Encapsulated validation in `Validate()` method called from constructor
- Contains value object `Dimensions` and navigational property to `Category`

### Data Layer Pattern

- **DbContext as UnitOfWork**: `CatalogContext` implements `IUnitOfWork` (src/Services/Catalog/Ddd.Catalog.Data/CatalogContext.cs)
  - Uses EF Core with fluent configuration
  - Global string convention: varchar(100), non-Unicode
  - Configuration classes in separate mapping files

- **Entity Configurations**: Implement `IEntityTypeConfiguration<T>` (src/Services/Catalog/Ddd.Catalog.Data/Mappings/)
  - Applied automatically via `ApplyConfigurationsFromAssembly()`
  - Value Objects mapped with `OwnsOne()` (e.g., Dimensions embedded in Product table)

## Common Commands

### Build
```bash
dotnet build DomainDrivenDesign.sln
```

### Run Tests
```bash
# Run all tests
dotnet test

# Run tests for a specific project
dotnet test src/Services/Catalog/Ddd.Catalog.Test/Ddd.Catalog.Test.csproj
```

### Add New Migration (when working with Catalog)
```bash
dotnet ef migrations add <MigrationName> --project src/Services/Catalog/Ddd.Catalog.Data --startup-project src/Services/Catalog/Ddd.Catalog.Data
```

### Update Database
```bash
dotnet ef database update --project src/Services/Catalog/Ddd.Catalog.Data --startup-project src/Services/Catalog/Ddd.Catalog.Data
```

## Development Guidelines

### Adding a New Bounded Context

Follow the existing Catalog bounded context structure:
1. Create three projects: `Ddd.[Context].Domain`, `Ddd.[Context].Data`, `Ddd.[Context].Test`
2. Domain project references only `Ddd.Core`
3. Data project references Domain project and EF Core packages
4. Test project references Domain project and xUnit
5. Add projects to solution file and nest under appropriate solution folders

### Creating Domain Entities

1. Inherit from `Entity` for entities or just define plain classes for value objects
2. Implement `IAggregateRoot` on the aggregate root
3. Use private setters and constructor validation
4. Call `AssertionConcern` static methods for validation in constructors and business methods
5. Implement rich domain behavior as public methods
6. Validate invariants in constructor and call `Validate()` method

### Entity Framework Configuration

1. Create mapping classes implementing `IEntityTypeConfiguration<T>` in `Mappings/` folder
2. Use `OwnsOne()` for value objects that should be embedded in the same table
3. Configure relationships explicitly with `HasOne()` / `WithMany()`
4. Override `ConfigureConventions()` in DbContext for global conventions
5. Let `ApplyConfigurationsFromAssembly()` discover all configurations automatically

### Testing

- Uses xUnit framework
- Test projects target .NET 9.0
- Coverage collection enabled via coverlet.collector
- Focus on domain logic testing (business rules, validations)
