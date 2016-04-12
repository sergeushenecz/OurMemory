using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurMemory.Domain.Entities
{
    public class DomainObject : IDomainObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }

    public interface IDomainObject
    {
        bool IsDeleted { get; set; }
    }
}