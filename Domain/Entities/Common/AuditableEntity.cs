namespace Domain.Entities.Common
{
    public class AuditableEntity
    {
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
