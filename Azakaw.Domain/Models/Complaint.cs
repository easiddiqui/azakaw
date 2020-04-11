using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azakaw.Domain.Models
{
    public class Complaint : BaseEntity
    {
        public int UserId { get; set; }
        [Required, MaxLength(500)]
        public string Message { get; set; }
        public ComplaintStatus ComplaintStatus { get; set; }

        [NotMapped]
        public virtual User User { get; set; }
    }
}