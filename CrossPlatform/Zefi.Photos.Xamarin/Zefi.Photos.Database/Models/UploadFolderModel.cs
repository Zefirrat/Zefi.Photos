using SQLite;

namespace Zefi.Photos.Database.Models
{
    [Table(nameof(UploadFolderModel))]
    public class UploadFolderModel
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string FolderPath { get; set; }
    }
}