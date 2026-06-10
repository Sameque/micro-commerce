namespace MicroCommerce.BuildingBlocks.SharedKernel;

public record DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
