using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace IndigentRegister
{
    class customDownloadListener : IDownloadListener
    {
       


        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            
        }

        public void OnDownloadStart(string url, string userAgent, string contentDisposition, string mimetype, long contentLength)
        {
            DownloadManager.Request request = new DownloadManager.Request(Android.Net.Uri.Parse(url));
            request.AllowScanningByMediaScanner();
            request.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);
            request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, "download");
            DownloadManager dm = (DownloadManager) Application.Context.GetSystemService(Context.DownloadService);
            dm.Enqueue(request);
        }
    }
}