namespace MicroCommerce.BuildingBlocks.Contracts;

/// <summary>
/// Base class for all integration events.
/// Integration events are used for communication between different microservices.
/// They must be immutable and are defined as records.
/// </summary>
public abstract record IntegrationEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public Guid CorrelationId { get; init; }
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    public int Version { get; init; } = 1;
}
