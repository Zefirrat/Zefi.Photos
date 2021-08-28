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
        public async Task<List<UploadFolderModel>> GetItemsAsync()
        {
            return await Database.Table<UploadFolderModel>().ToListAsync();
        }

        public async Task<List<UploadFolderModel>> GetItemsSqlAsync()
        {
            // SQL queries are also possible
            return await Database.QueryAsync<UploadFolderModel>("SELECT * FROM [UploadFolderModel] ");
        }

        public async Task<UploadFolderModel> GetItemAsync(int id)
        {
            return await Database.Table<UploadFolderModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(UploadFolderModel item)
        {
            var exist = await Database.Table<UploadFolderModel>().FirstOrDefaultAsync(i => i.Id == item.Id);
            if (exist!=null)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(UploadFolderModel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}