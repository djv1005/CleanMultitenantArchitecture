

namespace CleanMultitenantArchitecture.Domain.Entities
{
    public class Organization:BaseEntity<long>
    {

        public string Name { get; set; }

        public string Server { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<UserOrganization> UserOrganizations { get; set; }


    }
}
