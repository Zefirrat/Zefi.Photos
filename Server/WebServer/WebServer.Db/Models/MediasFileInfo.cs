using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebServer.Db.Enums;

namespace WebServer.Db.Models
{
    public class MediasFileInfo
    {
        private MediasFileInfo()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MediasFileInfoId { get; set; }
        
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public FileExtension FileExtension { get; set; }
        public string FileStorePath { get; set; }
        public User UserUploaded { get; set; }
    }
}