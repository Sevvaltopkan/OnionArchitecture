namespace OnionVb02.Domain.Entities
{
    public class AppUserProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
