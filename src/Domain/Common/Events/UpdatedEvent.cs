using SoftSquare.AlAhlyClub.Domain.Common.Entities;

namespace SoftSquare.AlAhlyClub.Domain.Common.Events;

public class UpdatedEvent<T> : DomainEvent where T : IEntity
{
    public UpdatedEvent(T entity)
    {
        Entity = entity;
    }

    public T Entity { get; }
}