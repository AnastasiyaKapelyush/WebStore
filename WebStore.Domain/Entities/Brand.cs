using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)] // уникальность поля
    //[Table("brandss")] // изменение наименования таблицы в БД
    public class Brand: NamedEntity, IOrderedEntity
    {
        //(Column("BrandOrder")) //Изменение имени столбца
        public int Order { get; set; }
    }
}
