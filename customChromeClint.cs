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
    [Register("android/webkit/WebChromeClient", DoNotGenerateAcw = true)]
    class customChromeClient : WebChromeClient 
    {
      
        static int FILECHOOSER_RESULTCODE = 1;
        IValueCallback mUploadMessage;

      

        private void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (data != null)
            {
                if (requestCode == FILECHOOSER_RESULTCODE)
                {
                    if (null == mUploadMessage || data == null)
                        return;
                    mUploadMessage.OnReceiveValue(WebChromeClient.FileChooserParams.ParseResult((int)resultCode, data));
                    mUploadMessage = null;
                }
            }
        }



        [Android.Runtime.Register("onShowFileChooser", "(Landroid/webkit/WebView;Landroid/webkit/ValueCallback;Landroid/webkit/WebChromeClient$FileChooserParams;)Z", "GetOnShowFileChooser_Landroid_webkit_WebView_Landroid_webkit_ValueCallback_Landroid_webkit_WebChromeClient_FileChooserParams_Handler")]
        public override bool OnShowFileChooser(Android.Webkit.WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            var appActivity = Android.App.Application.Context as MainActivity;
            mUploadMessage = filePathCallback;
            Intent chooserIntent = fileChooserParams.CreateIntent();
            //appActivity.StartActivity(chooserIntent, FILECHOOSER_RESULTCODE, OnActivityResult);




            //return base.OnShowFileChooser (webView, filePathCallback, fileChooserParams);
            return true;
        }

        public override void OnProgressChanged(WebView view, int newProgress)
        {
            //Toast toast = Toast.MakeText(Android.App.Application.Context, "Chrome here", ToastLength.Short);
           
            //toast.Show();
            base.OnProgressChanged(view, newProgress);
        }

        

    }
}