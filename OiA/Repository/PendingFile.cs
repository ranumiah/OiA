using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OiA.Repository
{
    public class PendingFile
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string FileFullName { get; set; }
    }
}
