using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OiA.Repository
{
    public class FileDetail
    {
        [Key]
        [Column(Order =  1)]
        [Required]
        public long Id { get; set; }

        [Column(Order = 2)]
        [MaxLength(32)]
        public string Md5Hash { get; set; }

        [Column(Order = 3)]
        [MaxLength(64)]
        public string Sha256Hash { get; set; }

        [Column(Order = 4)]
        [MaxLength(128)]
        public string Sha512Hash { get; set; }

        [Column(Order = 5)]
        [Required]
        public string FileName { get; set; }

        [Column(Order = 6)]
        [Required]
        public string FileFullName { get; set; }

        [Column(Order = 7)]
        [Required]
        public long FileLength { get; set; }

        [Column(Order = 8)]
        [Required]
        public string FileExtension { get; set; }

        [Column(Order = 9)]
        public DateTime FileCreationTimeUtc { get; set; }

        [Column(Order = 10)]
        public DateTime FileLastWriteTimeUtc { get; set; }

        [Column(Order = 11)]
        public DateTime FileLastAccessTimeUtc { get; set; }

        [Timestamp]
        [Column(Order = 101)]
        public Byte[] TimeStamp { get; set; }
    }
}
