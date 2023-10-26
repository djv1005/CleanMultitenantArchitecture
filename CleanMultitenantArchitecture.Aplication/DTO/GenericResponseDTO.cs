using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Aplication.DTO
{
    public class GenericResponseDTO<T> where T : class
    {

        public GenericResponseDTO() {
        }
        
        public int Status { get; set; }

        public string StatusText { get; set; }

        public string DetailedStatus { get; set; }

        public IEnumerable<T> DataList { get; set; }

        public T Data { get; set; }






    }
}
