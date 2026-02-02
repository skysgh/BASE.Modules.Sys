# About

Each [Logical] Module is composed of a small set of assemblies:

- `*.Common`
- `*.Infrastructure`
- `*.Application`
- `*.Presentation`

This is the Module's `*.Common` assembly.

It contains:

- Module Entities (internal, persisted to Database)
  - Each with at least a Guid ID field
- Module DTOs (external, to which Entities are converted to in for API use)
  - with some or all the properties on an entity
  - which should be only changed carefully to not introduce API Client breaking changes

