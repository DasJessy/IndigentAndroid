using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android.Webkit;
using Android.Net;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;

namespace IndigentRegister
{
    [Activity(Label = "Indigent Register", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity, IOnMenuTabClickListener
    {

        public static bool userGoBack = false;
        public static BottomBar _bottomBar;
        public static DrawerLayout drawerLayout;
        public static WebView webview;
        public static int count = 0;
        IndigentRegister.myWebClient myWebClient;

        public static GridLayout layoutUploadDownload;
        public static NavigationView navview;
        public static Android.Support.V7.Widget.Toolbar toolb;
        public static Button btnUpload, btnDigitalSignature, btnDownload;

        public static string previousUri = "http://mtraders.co.za/officer_mobile/login_mobile.aspx";

        public static string uriLOGIN = "http://mtraders.co.za/officer_mobile/login_mobile.aspx";

        public static string uri = "http://mtraders.co.za/officer_mobile/login_mobile.aspx";
        //public static string uri = "file:///android_asset/input.html";
        
        public static string uriA = "http://mtraders.co.za/officer_mobile/Applicants_mobile.aspx";
        public static string uriA2 = "http://mtraders.co.za/DataCapturers_Mobile/Applicants_mobile.aspx";
        public static string uriD = "http://mtraders.co.za/officer_mobile/Main_mobile.aspx";
        public static string uriR = "http://mtraders.co.za/Indigentsreport.aspx";


        //Override the go back function
        public override void OnBackPressed()
        {


            if (webview.CanGoBack() && userGoBack)
            {
                webview.GoBack();
            }



        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var chrome = new FileChooserWebChromeClient((uploadMsg, acceptType, capture) =>
            {
                //mUploadMessage = uploadMsg;
                //var i = new Intent(Intent.ActionGetContent);
                //i.AddCategory(Intent.CategoryOpenable);
                //i.SetType("application/pdf");
                //StartActivityForResult(Intent.CreateChooser(i, "File Chooser"), FILECHOOSER_RESULTCODE);
            });
            // Create UI
            SetContentView(Resource.Layout.Main);



            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            navview = FindViewById<NavigationView>(Resource.Id.nav_view);
            navview.Visibility = ViewStates.Gone;




            //layoutUploadDownload = FindViewById<GridLayout>(Resource.Id.grdUploadDownload);
            //layoutUploadDownload.Visibility = ViewStates.Gone;

            btnUpload = FindViewById<Button>(Resource.Id.btnUpload);
            btnUpload.Click += BtnUpload_Click;
            btnUpload.Visibility = ViewStates.Invisible;

            btnDigitalSignature = FindViewById<Button>(Resource.Id.btnDigitalSignature);
            btnDigitalSignature.Visibility = ViewStates.Gone;
            btnDigitalSignature.Click += BtnDigitalSignature_Click;


            //Download handler
            btnDownload = FindViewById<Button>(Resource.Id.btnDownload);
            btnDownload.Visibility = ViewStates.Gone;
            btnDownload.Click += BtnDownload_Click;

            toolb = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolb.Visibility = ViewStates.Gone;

            webview = FindViewById<WebView>(Resource.Id.webview);
            webview.Download += Webview_Download;
            //webview.SetDownloadListener(new customDownloadListener());
            webview.SetWebChromeClient(chrome);
            /*webview.SetWebChromeClient(new customChromeClient(

            //    uploadMsg => {
            //    mUploadMessage = uploadMsg;
            //    var intent = new Intent(Intent.ActionGetContent);
            //    intent.AddCategory(Intent.CategoryOpenable);
            //    intent.SetType("image/*");
            //    StartActivityForResult(Intent.CreateChooser(intent, "File Chooser"),
            //        FilechooserResultcode);
            //}
            ));*/


            webview.Settings.JavaScriptEnabled = true;
            webview.Settings.AllowContentAccess = true;
            webview.Settings.SetPluginState(WebSettings.PluginState.On);
            webview.Settings.AllowFileAccess = true;
            webview.Settings.AllowFileAccessFromFileURLs = true;
            webview.Settings.AllowUniversalAccessFromFileURLs = true;
            webview.Settings.AllowContentAccess = true;
            webview.Settings.DatabaseEnabled = true;
            webview.Settings.DomStorageEnabled = true;
            webview.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            webview.Settings.LoadsImagesAutomatically = true;
            webview.Settings.SetGeolocationEnabled(true);
            webview.Settings.SetSupportMultipleWindows(true);
            webview.Settings.SetSupportZoom(false);
            webview.Settings.DomStorageEnabled = true;
            myWebClient = new IndigentRegister.myWebClient(this);
            webview.SetWebViewClient(myWebClient);
            webview.LoadUrl(uri);


            // Init toolbar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();


            //Bottom bar
            //_bottomBar = new BottomBar(this);
            ////_bottomBar = BottomBar.Attach(FindViewById(Resource.Id.webview), bundle);
            //_bottomBar.SetOnMenuTabClickListener(this);
            //_bottomBar.SetFitsSystemWindows(true);
            //_bottomBar.SetTextAppearance(Resource.Style.TextAppearance_AppCompat);


            //_bottomBar.SetItems(Resource.Menu.bottomMenu);
            //_bottomBar.NoTabletGoodness();





            //// Setting colors for different tabs when there's more than three of them.
            //// You can set colors for tabs in three different ways as shown below.
            //_bottomBar.MapColorForTab(0, "#006666");
            //_bottomBar.MapColorForTab(1, "#00bc00");
            //_bottomBar.MapColorForTab(2, "#0060fc");
            //_bottomBar.MapColorForTab(3, "#FF5252");
            //_bottomBar.Show(false);
            ////_bottomBar.MapColorForTab(4, "#FF9800");
            //_bottomBar.Visibility = ViewStates.Visible;

            //_bottomBar.SetActiveTabColor("#006666");
            //_bottomBar.Enabled = false;


        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();

        }

        private void BtnDigitalSignature_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SiginatureDefinitioncs dgSign = new SiginatureDefinitioncs();

            dgSign.Show(transaction, "Indigent Signatures");
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogUpload dgUpload = new DialogUpload();
            dgUpload.Show(transaction, "Indigent File Upload");


            //mUploadMessage = uploadMsg;

        }

        private void Webview_Download(object sender, DownloadEventArgs e)
        {
            try
            {
                var source = Android.Net.Uri.Parse(e.Url);

                var request = new DownloadManager.Request(source);

                request.AllowScanningByMediaScanner();

                request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);

                request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, source.LastPathSegment);

                var manager = (DownloadManager)this.GetSystemService(Context.DownloadService);

                manager.Enqueue(request);

                Toast.MakeText(this, "Downloading Indigent Reports", ToastLength.Short).Show();
            }

            catch (Exception ex)
            {
                Toast.MakeText(this, "Cannot Download the file", ToastLength.Short).Show();
                Console.WriteLine(ex.Message);

            };
        }

        //protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{

        //    base.OnActivityResult(requestCode, resultCode, data);


        //    if (requestCode == FilechooserResultcode)
        //    {
        //        if (mUploadMessage == null)
        //            return;
        //        var result = data == null || resultCode != Result.Ok
        //            ? null
        //            : data.Data;
        //        //SetHtml(result.ToString());
        //        mUploadMessage.OnReceiveValue(result);
        //        mUploadMessage = null;
        //    }


        //}

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            webview.RestoreState(savedInstanceState);

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            webview.SaveState(outState);
            //_bottomBar.OnSaveInstanceState(outState);
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_dashboard):

                    previousUri = webview.Url;

                    webview.LoadUrl(uriD);
                    uri = uriD;

                    break;
                case (Resource.Id.nav_Indigents):
                    // React on 'Messages' selection
                    previousUri = webview.Url;

                    webview.LoadUrl(uriA);
                    uri = uriA;
                    break;
                case (Resource.Id.nav_Reports):

                    previousUri = webview.Url;

                    webview.LoadUrl(uriR);
                    uri = uriR;

                    // mUploadMessage = uploadMsg;
                    //var intent = new Intent(Intent.ActionGetContent);
                    //intent.AddCategory(Intent.CategoryOpenable);
                    //intent.SetType("image/*");
                    //StartActivityForResult(Intent.CreateChooser(intent, "File Chooser"),
                    //    FilechooserResultcode);


                    break;
                case (Resource.Id.nav_Logout):

                    uri = uriLOGIN;
                    previousUri = null;
                    webview.LoadUrl(uri);
                    MainActivity.navview.Visibility = ViewStates.Gone;
                    MainActivity.toolb.Visibility = ViewStates.Gone;
                    //userGoBack = false;

                    // React on 'Discussion' selection
                    break;
            }

            // Close drawer
            drawerLayout.CloseDrawers();
        }

        public void OnMenuTabSelected(int menuItemId)
        {
            switch (menuItemId)
            {
                case 0:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;
                case 1:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;
                case 2:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;
                case 3:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;


            };

        }

        public void OnMenuTabReSelected(int menuItemId)
        {
            switch (menuItemId)
            {
                case 0:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;
                case 1:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;
                case 2:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;
                case 3:
                    Toast.MakeText(this, "Tab selected - " + menuItemId.ToString(), ToastLength.Short).Show();
                    break;


            };

        }
    }
    }