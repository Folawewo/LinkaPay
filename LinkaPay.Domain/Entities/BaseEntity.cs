using System;
namespace LinkaPay.Domain.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
    }
}
