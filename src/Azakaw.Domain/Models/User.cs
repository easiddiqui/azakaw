using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azakaw.Domain.Models
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public Role Role { get; set; }

        [NotMapped]
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}