using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Domain.Entities
{
    public class Product:BaseEntity<long>,IMustHaveTenant
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public long TenantId { get; set; }

        public Organization Organization { get; set; }  

    }
}
