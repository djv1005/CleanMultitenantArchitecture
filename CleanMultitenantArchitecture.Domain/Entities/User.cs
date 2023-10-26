using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Domain.Entities
{
    public class User:BaseEntity<long>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<UserOrganization> UserOrganizations { get; set; }

    }
}
