using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Zefi.Photos.Xamarin.Annotations;

namespace Zefi.Photos.Xamarin.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string Text { get; set; }
        public ImageSource Image { get; set; }
        public ICommand PickFileCommand 
        {
            get => _pickFileCommand/* ??  new Command(async () => await PickAndShow())*/;
            set => _pickFileCommand = value;
        }

        private ICommand _pickFileCommand;

        // async Task<FileResult> PickAndShow()
        // {
        //     var options = new PickOptions();
        //     options.FileTypes = FilePickerFileType.Images;
        //     options.PickerTitle = "Select file in folder that you wish to choose.";
        //     
        //     try
        //     {
        //         var result = await FilePicker.PickAsync(options);
        //         if (result != null)
        //         {
        //             Text = $"File Name: {result.FileName}";
        //             if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
        //                 result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
        //             {
        //                 var stream = await result.OpenReadAsync();
        //                 Image = ImageSource.FromStream(() => stream);
        //             }
        //         }
        //
        //         return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         // The user canceled or something went wrong
        //     }
        //
        //     return null;
        // }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}