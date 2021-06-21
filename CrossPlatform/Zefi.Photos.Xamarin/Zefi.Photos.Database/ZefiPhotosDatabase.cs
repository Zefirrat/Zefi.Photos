using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Zefi.Photos.Database.Helpers;
using Zefi.Photos.Database.Models;

namespace Zefi.Photos.Database
{
    public class ZefiPhotosDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ZefiPhotosDatabase> Instance = new AsyncLazy<ZefiPhotosDatabase>(async () =>
        {
            var instance = new ZefiPhotosDatabase();
            CreateTableResult result = await Database.CreateTableAsync<UploadFolderModel>();
            return instance;
        });

        public ZefiPhotosDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
        public Task<List<UploadFolderModel>> GetItemsAsync()
        {
            return Database.Table<UploadFolderModel>().ToListAsync();
        }

        public Task<List<UploadFolderModel>> GetItemsSqlAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<UploadFolderModel>("SELECT * FROM [UploadFolderModel] ");
        }

        public Task<UploadFolderModel> GetItemAsync(int id)
        {
            return Database.Table<UploadFolderModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(UploadFolderModel item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(UploadFolderModel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}