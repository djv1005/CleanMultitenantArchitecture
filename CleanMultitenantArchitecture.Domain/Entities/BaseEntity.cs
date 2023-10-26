

namespace CleanMultitenantArchitecture.Domain.Entities
{
    public class BaseEntity<T> where T : struct
    {
        public T Id { get; set; }

        public DateTime CreatedDate { get; set; }

        
    }
}
