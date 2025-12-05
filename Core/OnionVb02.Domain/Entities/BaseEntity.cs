using OnionVb02.Domain.Enums;
using OnionVb02.Domain.Interfaces;

namespace OnionVb02.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }
    }
}
