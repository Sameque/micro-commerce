namespace MicroCommerce.BuildingBlocks.Contracts;

/// <summary>
/// Provides a common way to handle event metadata across the system.
/// </summary>
public record EventMetadata(
    Guid EventId,
    Guid CorrelationId,
    DateTime OccurredAt,
    int Version
);
