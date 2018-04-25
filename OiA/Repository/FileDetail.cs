using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OiA.Repository
{
    public class FileDetail
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [MaxLength(32)]
        public string Md5Hash { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public long Length { get; set; }

        [Required]
        public string Extension { get; set; }

        public DateTime FileCreationTimeUtc { get; set; }

        public DateTime FileLastWriteTimeUtc { get; set; }

        public DateTime FileLastAccessTimeUtc { get; set; }

        [Required]
        public string Status { get; set; } = ProcessStatus.New;
    }
}
