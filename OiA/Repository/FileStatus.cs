using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OiA.Repository
{
    public class FileStaging
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string FileFullName { get; set; }

        [Key] [Column(Order = 1)] [Required] public string Status { get; set; } = ProcessStatus.New;
    }
}
