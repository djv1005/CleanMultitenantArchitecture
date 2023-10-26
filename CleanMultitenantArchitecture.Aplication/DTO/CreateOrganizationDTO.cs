using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Aplication.DTO
{
    public class CreateOrganizationDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Server { get; set; }

        [Required]
        public string User { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
