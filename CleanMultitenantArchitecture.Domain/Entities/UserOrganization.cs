

namespace CleanMultitenantArchitecture.Domain.Entities
{
    public class UserOrganization:BaseEntity<long>
    {
        public long OrganizationFk { get; set; }

        public long UserFk { get; set; }

        public Organization Organization { get; set; }

        public User User { get; set; }
    }
}
