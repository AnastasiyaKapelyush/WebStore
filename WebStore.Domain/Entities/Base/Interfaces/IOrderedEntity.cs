namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Упорядочиваемая сущность
    /// </summary>
    public interface IOrderedEntity : IEntity
    { 
        int Order { get; }
    }
}
