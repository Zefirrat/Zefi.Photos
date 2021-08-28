using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Zefi.Photos.Database;
using Zefi.Photos.Database.Models;
using Zefi.Photos.Xamarin.Annotations;

namespace Zefi.Photos.Xamarin.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public ImageSource Image { get; set; }

        public ICommand AddFilePathCommand
        {
            get => _pickFileCommand ?? new Command(async () => await _addFilePathCommand());
            set => _pickFileCommand = value;
        }

        public ICommand ClearListCommand
        {
            get => _clearListCommand ?? new Command(async () => await ClearList());
            set => _clearListCommand = value;
        }

        public MainPageViewModel()
        {
            DrawText();
        }

        private ICommand _pickFileCommand;
        private string _text;
        private ICommand _clearListCommand;

        private string _folderEntry;

        public string FolderEntry
        {
            get => _folderEntry;
            set { _folderEntry = value; }
        }

        private async Task DrawText()
        {
            var db = await ZefiPhotosDatabase.Instance;
            var items = await db.GetItemsSqlAsync();
            var itemsString = string.Empty;

            foreach (var uploadFolderModel in items)
            {
                itemsString = $"{itemsString}\n{uploadFolderModel.FolderPath}";
            }

            Text = itemsString;
        }

        private async Task ClearList()
        {
            var db = await ZefiPhotosDatabase.Instance;
            var items = await db.GetItemsSqlAsync();
            foreach (var uploadFolderModel in items)
            {
                await db.DeleteItemAsync(uploadFolderModel);
            }

            await DrawText();
        }

        async Task<FileResult> PickAndShow()
        {
            var options = new PickOptions();
            options.FileTypes = FilePickerFileType.Images;
            options.PickerTitle = "Select file in folder that you wish to choose.";

            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    var resultFullPath = result.FullPath;
                    await _addStringToDb(resultFullPath);
                }

                return result;
            }
            catch (Exception ex)
            {
                Text = ex.Message;
            }

            return null;
        }

        private async Task _addFilePathCommand()
        {
            await _addStringToDb(FolderEntry);
        }

        private async Task _addStringToDb(string resultFullPath)
        {
            ZefiPhotosDatabase db = await ZefiPhotosDatabase.Instance;
            var items = await db.GetItemsSqlAsync();
            await db.SaveItemAsync(new UploadFolderModel {Id = items.Count, FolderPath = resultFullPath});
            await DrawText();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}