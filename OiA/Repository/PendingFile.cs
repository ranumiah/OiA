using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OiA.Repository
{
    public class PendingFile
    {
        [Key]
        [Required]
        public string FileFullName { get; set; }

        [Required]
        public string Status { get; set; } = ProcessStatus.New;
    }
}
