namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Сущность, у которой есть имя
    /// </summary>
    public interface INamedEntity: IEntity
    { 
        string Name { get; }
    }
}
