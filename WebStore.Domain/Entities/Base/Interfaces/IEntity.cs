namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    /// Под сущностью понимаем все, что имеет идентификатор
    /// </summary>
    public interface IEntity
    {
        int Id { get; }
    }
}
