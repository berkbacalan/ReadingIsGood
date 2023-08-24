using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingIsGood.Domain.Entities.Base;

public abstract class Entity : IEntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; protected set; }
}