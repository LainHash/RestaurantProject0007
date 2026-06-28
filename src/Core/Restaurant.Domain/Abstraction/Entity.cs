namespace Restaurant.Domain.Abstraction
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }

    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public abstract class SoftDeleteEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
