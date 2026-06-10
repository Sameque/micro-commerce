namespace MicroCommerce.BuildingBlocks.SharedKernel;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
