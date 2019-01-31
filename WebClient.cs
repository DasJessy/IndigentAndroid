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
using Android.Graphics;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using System.Net;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using Java.IO;

namespace IndigentRegister
{
    

    class myWebClient : WebViewClient
    {

        public Activity _context;
        ProgressDialog progress;
        private Boolean loadError = false;
        private int count = 0;
        public string fileID;
        public static byte[] fileByte = default(byte[]);

        public myWebClient(Activity context)
        {
            _context = context;
            progress = new ProgressDialog(context);
            
            
        }
        
        //Override the error Page
        public override void OnReceivedError(WebView view, [GeneratedEnum] ClientError errorCode, string description, string failingUrl)
        {
            loadError = true;
            view.LoadUrl("ABOUT:BLANK");
            

        }

        //Check the connection
        public static Boolean isNetworkStatusAvialable(Context context)
        {
            Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            if (connectivityManager != null)
            {
                Android.Net.NetworkInfo netInfos = connectivityManager.ActiveNetworkInfo;
                if (netInfos != null)
                    if (netInfos.IsConnected)
                        return true;
            }
            return false;
        }
       

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            


            if (view.Url == MainActivity.uriA)
            {
                return false;
            }
            if (view.Url == MainActivity.uriA2)
            {
                
                return false;
            }
            else
            {
                view.LoadUrl(url);
                    return true;
            }
                
        }


        /*
                public override bool ShouldOverrideUrlLoading(WebView view, string url)
                {
                    // bool shouldoverride = false;
                    // view.LoadUrl(url);
                   // bool value = true;
                   /*
                    String extension = MimeTypeMap.GetFileExtensionFromUrl(url);





                        if (url.EndsWith(".jpg"))
                    {
                        //shouldoverride = true;
                        progress.SetMessage("Downloading");

                        Android.Net.Uri source = Android.Net.Uri.Parse(url);
                        DownloadManager.Request request = new DownloadManager.Request(source);
                        request.AllowScanningByMediaScanner();
                        request.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);
                        request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, "download");
                        DownloadManager dm = (DownloadManager)Application.Context.GetSystemService(Context.DownloadService);
                        request.SetDescription("Indigent File");
                        request.SetTitle("Download");
                        dm.Enqueue(request);

                        Toast toast = Toast.MakeText(Android.App.Application.Context, "Indigent File Downloaded", ToastLength.Short);

                        toast.Show();

                    }
                    if(url.EndsWith(".jpeg"))
                    {
                        progress.SetMessage("Downloading");
                        Android.Net.Uri source = Android.Net.Uri.Parse(url);
                        DownloadManager.Request request = new DownloadManager.Request(source);
                        request.AllowScanningByMediaScanner();
                        request.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);
                        request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, "download");
                        DownloadManager dm = (DownloadManager)Application.Context.GetSystemService(Context.DownloadService);
                        request.SetDescription("Indigent File");
                        request.SetTitle("Download");
                        dm.Enqueue(request);

                        Toast toast = Toast.MakeText(Android.App.Application.Context, "Indigent File Downloaded", ToastLength.Short);

                        toast.Show();
                    }
                    if (url.EndsWith(".pdf"))
                    {
                        progress.SetMessage("Downloading");
                        Android.Net.Uri source = Android.Net.Uri.Parse(url);
                        DownloadManager.Request request = new DownloadManager.Request(source);
                        request.AllowScanningByMediaScanner();
                        request.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);
                        request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, "download");
                        DownloadManager dm = (DownloadManager)Application.Context.GetSystemService(Context.DownloadService);
                        request.SetDescription("Indigent File");
                        request.SetTitle("Download");
                        dm.Enqueue(request);

                        Toast toast = Toast.MakeText(Android.App.Application.Context, "Indigent File Downloaded", ToastLength.Short);

                        toast.Show();
                    }
                    else
                    {
                        view.LoadUrl(url);
                    }


                    return false;
                }*/

        public override void OnFormResubmission(WebView view, Message dontResend, Message resend)
        {
            resend.SendToTarget();
        }

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {


            //if (view.Url.Contains("http://mtraders.co.za/officer_mobile/Applicants_mobile.aspx") || view.Url.Contains("http://mtraders.co.za/DataCapturers_Mobile/Applicants_mobile.aspx"))
            //{
            //    MainActivity.layoutUploadDownload.Visibility = ViewStates.Visible;
            //}
            //else
            //{

            //    MainActivity.layoutUploadDownload.Visibility = ViewStates.Gone;
            //}

            Toast.MakeText(_context, "Please Wait...", ToastLength.Short).Show();
            
            if (url != MainActivity.uriLOGIN)
            {
                MainActivity.userGoBack = true;
            }
            else
            {
                MainActivity.userGoBack = false;
            }


            loadError = false;

            progress.SetMessage("Loading");

            if (view.Url == MainActivity.uriD)
            {
               
                progress.SetMessage("Loading Dashboard");
                progress.Show();
            }
            if(view.Url == MainActivity.uriA || view.Url == MainActivity.uriA2)
            {
                progress.SetMessage("Loading Indigents");
                progress.Show();
            }
            if(view.Url == MainActivity.uriR)
            {
                progress.SetMessage("Loading Reports");
                progress.Show();
            }
            if(view.Url == MainActivity.uriLOGIN)
            {
                

                if (view.Progress == 50)
                {
                    progress.SetMessage("Please Wait");
                    progress.Show();
                }
                else
                {
                    progress.SetMessage("Connecting...");
                    progress.Show();
                }
                
               
            }
            else
            {
               
                progress.SetIcon(Resource.Drawable.Icon);
                progress.Show();
            }
               
            

            if (view.Url == MainActivity.uriLOGIN)
            {

                //MainActivity.toolb.Visibility = ViewStates.Invisible;
                //MainActivity.navview.Visibility = ViewStates.Invisible;
                
              //  MainActivity.drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);

            }
            else
            {
               //MainActivity.toolb.Visibility = ViewStates.Visible;
                //MainActivity.navview.Visibility = ViewStates.Visible;
                //MainActivity.drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedOpen);
            }

            //if (isNetworkStatusAvialable(_context) == true)
            //{
            //    base.OnPageStarted(view, url, favicon);
            //    progress.SetMessage("Loading");
            //    progress.Show();
            //}
            //else
            //{
            //    //set alert for executing the task
            //    AlertDialog.Builder alert = new AlertDialog.Builder(_context);
            //    alert.SetTitle("Error");
            //    alert.SetMessage("No Internet Connection");
            //    alert.SetPositiveButton("Retry", (senderAlert, args) => {
            //        Toast.MakeText(_context, "Attempting to connect", ToastLength.Short).Show();

            //    });
            //    alert.SetNegativeButton("Cancel", (senderAlert, args) => {
            //        Toast.MakeText(_context, "Cancelled!", ToastLength.Short).Show();
            //    });
            //    Dialog dialog = alert.Create();
            //    dialog.Show();
            //}
            //base.OnPageStarted(view, url, favicon);
        }

        public bool downloadFile(string fileid)
        {
            fileByte = null;
            var filename = "";
            System.Net.ServicePointManager.Expect100Continue = false;
            string con_string =SQL_Functions.GetConnectionstring();
            using (SqlConnection conn = new SqlConnection())
            {
                SqlDataReader datareader;
                conn.ConnectionString = con_string;


                SqlCommand command = new SqlCommand("SELECT Name, Data FROM Files WHERE id ='" + fileid.ToString() + "';");
                command.Connection = conn;

                conn.Open();
                command.ExecuteNonQuery();
                datareader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        filename = datareader["Name"].ToString();
                        fileByte = (byte[])datareader["Data"];
                    }
                }
                conn.Close();

            }

            var downloadLoc = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads) + "'\'" + filename;
            Java.IO.File outputFile = new Java.IO.File(downloadLoc);

            try
            {
                if (outputFile.Exists())
                {
                    outputFile.CreateNewFile();
                }
                FileOutputStream outputStream = new FileOutputStream(outputFile);
                outputStream.Write(fileByte);  //write the bytes and im done. 

                var localImage = new Java.IO.File(downloadLoc);
                if (localImage.Exists())
                {

                    global::Android.Net.Uri uri = global::Android.Net.Uri.FromFile(localImage);

                    var intent = new Intent(Intent.ActionView, uri);
                    
                    intent.SetAction(Intent.ActionView);
                    //  intent.SetType ("application/pdf");
                    try
                    {
                        intent.SetData(global::Android.Net.Uri.FromFile(localImage));
                        intent.AddFlags(ActivityFlags.NewTask);
                        Android.App.Application.Context.StartActivity(intent);
                    }
                    catch(Exception e)
                    {
                        var msg = e.Message;
                        Toast.MakeText(_context, "Please open the file from the root : " + downloadLoc, ToastLength.Long).Show();
                    }
                   
                    

                }
            }
            catch (Exception ex)
            {
                new AlertDialog.Builder(_context).SetTitle("Indigent App Error").SetMessage("Upload failed: " + ex.Message + "\n \nPlease try again later").SetCancelable(true).Show();
            }



            return true;

        }

        private void downloadThread ()
          {
        Thread.Sleep (1000);
        _context.RunOnUiThread (() => downloadFile(fileID));
           
          }
    
        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);

            if (url.Contains("#"))
            {

                fileID = url.ToString().Split('#')[1];

                Toast.MakeText(_context, "Downloading File ", ToastLength.Short).Show();

                //ThreadPool.QueueUserWorkItem(o => downloadThread());

                downloadFile(fileID);

                //Thread thread = new Thread(downloadThread);
                // thread.Start();

                //downloadThread();

                Toast.MakeText(_context, "File downloaded ", ToastLength.Short).Show();
            }


            if (view.Url != MainActivity.uriLOGIN && view.Url != "ABOUT:BLANK")
            {
                MainActivity.navview.Visibility = ViewStates.Visible;
                MainActivity.toolb.Visibility = ViewStates.Visible;
                MainActivity.drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
                //MainActivity._bottomBar.Visibility = ViewStates.Visible;

            }
            else
            {
                MainActivity.navview.Visibility = ViewStates.Gone;
                MainActivity.drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
                MainActivity.toolb.Visibility = ViewStates.Gone;
               // MainActivity._bottomBar.Visibility = ViewStates.Gone;
            }

            if (loadError == true)
            {
                MainActivity.navview.Visibility = ViewStates.Gone;
                MainActivity.drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
               // MainActivity._bottomBar.Visibility = ViewStates.Gone;
                MainActivity.toolb.Visibility = ViewStates.Gone;
                Snackbar.Make(MainActivity.drawerLayout, "Please Check your internet connection and Retry again...", Snackbar.LengthIndefinite)
            .SetAction("Retry", (v) => { view.LoadUrl(MainActivity.uriLOGIN); })
             .Show();

            }

            if (view.Url.Contains("http://mtraders.co.za/DataCapturers_Mobile/Register_Applicant.aspx?RefNo=") || view.Url.Contains("http://mtraders.co.za/officer_mobile/Register_page_test2.aspx?RefNo="))
            {
                count++;
                MainActivity.btnUpload.Visibility = ViewStates.Visible;
                MainActivity.btnDigitalSignature.Visibility = ViewStates.Visible;
                Uri myUri = new Uri(view.Url, UriKind.RelativeOrAbsolute);
                var query = System.Web.HttpUtility.ParseQueryString(myUri.Query);
                SQL_Functions.RefNumber = query.Get("RefNo");

                
               
                
                if (count == 1)
                {
                    progress.SetMessage("Initialising Database Connection");
                    bool databaseConnection = SQL_Functions.intialiseApplicant();
                    bool initialiseOccupants = SQL_Functions.intialiseOccupant();
                    if (databaseConnection == true && initialiseOccupants == true)
                    {

                        Toast.MakeText(_context, "Applicants/Occupants Initialized", ToastLength.Long).Show();

                    }
                    else
                    {

                        Toast.MakeText(_context, "Error Occured during database Connection", ToastLength.Long).Show();

                    }

                }
               
                

              



            }
            else
            {
                count = 0;
                MainActivity.btnUpload.Visibility = ViewStates.Gone;
                MainActivity.btnDigitalSignature.Visibility = ViewStates.Gone;
            }

            progress.Dismiss();
        }





    }
}