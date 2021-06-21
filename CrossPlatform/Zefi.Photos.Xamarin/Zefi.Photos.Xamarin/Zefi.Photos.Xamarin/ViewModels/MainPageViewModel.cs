using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
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

        public ICommand PickFileCommand
        {
            get => _pickFileCommand ?? new Command(async () => await PickAndShow());
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

        private async Task DrawText()
        {
            var db = await ZefiPhotosDatabase.Instance;
            var items = await db.GetItemsAsync();
            var itemsString = string.Empty;

            foreach (var uploadFolderModel in items)
            {
                itemsString = $"{itemsString};{uploadFolderModel.FolderPath}";
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
                    ZefiPhotosDatabase db = await ZefiPhotosDatabase.Instance;
                    await db.SaveItemAsync(new UploadFolderModel {Id = 0, FolderPath = Path.GetDirectoryName(result.FullPath)});
                    await DrawText();
                }

                return result;
            }
            catch (Exception ex)
            {
                Text = ex.Message;
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}