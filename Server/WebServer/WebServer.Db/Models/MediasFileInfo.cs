using WebServer.Db.Enums;

namespace WebServer.Db.Models
{
    public class MediasFileInfo
    {
        private MediasFileInfo() {}

        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public FileExtension FileExtension { get; set; }
        public string FileStorePath { get; set; }
        public User UserUploaded { get; set; }
    }
}