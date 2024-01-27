namespace Domain.Entities.Common
{
    public class BaseEntity : AuditableEntity
    {
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
