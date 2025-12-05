using OnionVb02.Application.DTOInterfaces;
using OnionVb02.Domain.Enums;

namespace OnionVb02.Application.DTOClasses
{
    public abstract class BaseDto : IDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }
    }
}
