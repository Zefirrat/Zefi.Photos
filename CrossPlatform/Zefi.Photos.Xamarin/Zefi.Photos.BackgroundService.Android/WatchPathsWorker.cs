using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using AndroidX.Work;
using Xamarin.Android.Net;
using Zefi.Photos.Database;

namespace Zefi.Photos.BackgroundService
{
    public class WatchPathsWorker : Worker
    {
        public WatchPathsWorker(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public WatchPathsWorker(Context context, WorkerParameters workerParams) : base(context, workerParams)
        {
        }

        public override Result DoWork()
        {
            using var httpclient = new HttpClient(new AndroidClientHandler());
            httpclient.DefaultRequestHeaders.Add("Token", "MyDefaultToken");
            try
            {
                var response = httpclient.GetAsync("http://192.168.31.210:8765/PhotoUpload/TestConnection/")
                    .GetAwaiter().GetResult();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var db = ZefiPhotosDatabase.Instance.GetAwaiter().GetResult();

                    var paths = db.GetItemsAsync().GetAwaiter().GetResult();

                    foreach (var path in paths)
                    {
                        if (Directory.Exists(path.FolderPath))
                        {
                            var files = Directory.EnumerateFiles(path.FolderPath);
                            Android.Util.Log.Debug(nameof(WatchPathsWorker), files.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error file upload.");
            }
            finally
            {
            }

            return Result.InvokeSuccess();
        }
    }
}