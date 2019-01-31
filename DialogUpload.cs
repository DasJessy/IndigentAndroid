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
using System.IO;
using Android.Database;
using Android.Provider;

namespace IndigentRegister
{
    class DialogUpload : DialogFragment
    {

        public static Spinner spnType, spnName, spnContentType;
        public static String dType, dContentType, dName;
        public static EditText txtComments;
        public Button btnUploadDialog, btnChooseFilesDialog, btnGoBack;
        public TextView txtFileChoosen;
        private const int FILECHOOSER_RESULTCODE = 1;
        const int FilechooserResultcode = 1;
        IValueCallback mUploadMessage;
        private Action<int, Result, Intent> _resultCallback;
        public static string filepath = string.Empty;
        public static string filename = string.Empty;
        public static string filetype = string.Empty;


        public static string GetRealPathFromURI(Android.Net.Uri contentUri)
        {
            var mediaStoreImagesMediaData = "_data";
            string[] projection = { mediaStoreImagesMediaData };
            Android.Database.ICursor cursor = Application.Context.ContentResolver.Query(contentUri, projection, null, null, null);
            int columnIndex = cursor.GetColumnIndexOrThrow(mediaStoreImagesMediaData);
            cursor.MoveToFirst();
            return cursor.GetString(columnIndex);

            //ContentResolver cr = Application.Context.ContentResolver;

            //Android.Net.Uri uri = MediaStore.Files.GetContentUri("external");

            //// every column, although that is huge waste, you probably need
            //// BaseColumns.DATA (the path) only.
            //String[] projection = null;

            //// exclude media files, they would be here also.
            //String selection = MediaStore.Files.FileColumns.MediaType + "="
            //        + Android.po;
            //String[] selectionArgs = null; // there is no ? in selection so null here

            //String sortOrder = null; // unordered
            //Cursor allNonMediaFiles = cr.query(uri, projection, selection, selectionArgs, sortOrder);


        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {

            Dialog dialog = base.OnCreateDialog(savedInstanceState);

            dialog.SetTitle("Indigent(s) File Uploader");

            return dialog;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            //Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

           
            base.OnActivityCreated(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

           base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.uploadDialog2, container, false); ;

            return view;


        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {


            base.OnViewCreated(view, savedInstanceState);




            spnType = View.FindViewById<Spinner>(Resource.Id.lstAppOcc);
            spnName = View.FindViewById<Spinner>(Resource.Id.lstName);
            spnContentType = View.FindViewById<Spinner>(Resource.Id.lstDocumentType);
            btnChooseFilesDialog = View.FindViewById<Button>(Resource.Id.dbtn1);
            btnUploadDialog = View.FindViewById<Button>(Resource.Id.dbtn2);
            btnGoBack = view.FindViewById<Button>(Resource.Id.dbtn3);
            txtFileChoosen = view.FindViewById<TextView>(Resource.Id.txtFileChoosenDialog);
            txtComments = view.FindViewById<EditText>(Resource.Id.txtUploadComments);
            txtFileChoosen.Text = "No File Chosen";
            btnUploadDialog.Enabled = false;
            txtFileChoosen.TextChanged += TxtFileChoosen_TextChanged;
            btnUploadDialog.Click += DbtnUpload_Click;
            btnChooseFilesDialog.Click += DChooseFiles_Click;
            btnGoBack.Click += BtnGoBack_Click;
            addItemsOnSpinner();


        }

        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void TxtFileChoosen_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
           if(txtFileChoosen.Text.Contains("No File Chosen"))
            {
                btnUploadDialog.Enabled = false;
            }
            else
            {
                btnUploadDialog.Enabled = true;
            }
        }


        private string GetStoragePath(Android.Net.Uri uri)
        {
            string path = null;
            // The projection contains the columns we want to return in our query.
            string[] projection = new[] { Android.Provider.MediaStore.Audio.Media.InterfaceConsts.Data };
            using (ICursor cursor = Application.Context.ContentResolver.Query(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Audio.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }

        private void DChooseFiles_Click(object sender, EventArgs e)
        {
            //mUploadMessage = uploadMsg;
            var intent = new Intent(Intent.ActionGetContent);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("*/*");
            StartActivityForResult(Intent.CreateChooser(intent, "File Chooser"),
                FilechooserResultcode);

            btnUploadDialog.Text = "Upload";
           


        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case FILECHOOSER_RESULTCODE:
                    if (resultCode == Result.Ok)
                    {
                        Android.Net.Uri uri = data.Data;
                        
                        filepath = GetRealPathFromURI(uri);

                       // filepath = GetStoragePath(uri);

                        txtFileChoosen.Text = filepath;

                        //System.Uri tempuri = new Uri(filepath);

                        filename = Path.GetFileName(filepath);
                        //Toast.MakeText(Activity, "Filename: " + filename, ToastLength.Long).Show();

                        filetype = Path.GetExtension(filepath);
                        //Toast.MakeText(Activity, "Extension: " + filetype, ToastLength.Long).Show();

                        
                    }
                    break;



            }


            base.OnActivityResult(requestCode, resultCode, data);
        }

        //public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{
        //    if (requestCode == FILECHOOSER_RESULTCODE)
        //    {
        //        if (null == mUploadMessage)
        //            return;
        //        Java.Lang.Object result = data == null || resultCode != Result.Ok
        //            ? null
        //            : data.Data;
        //        mUploadMessage.OnReceiveValue(result);
        //        mUploadMessage = null;
        //    }
        //}

        //public void StartActivity(Intent intent, int requestCode, Action<int, Result, Intent> resultCallback)
        //{
        //    _resultCallback = resultCallback;
        //    StartActivityForResult(intent, requestCode);
        //}

        private void DbtnUpload_Click(object sender, EventArgs e)
        {
            Toast.MakeText(Activity, "Uploading " + filename, ToastLength.Long).Show();

            try
            {
                var comment = txtComments.Text;
               // WcfService_Indigent_Final.Service1 indigentWebService = new WcfService_Indigent_Final.Service1();

               //indigentWebService.Indigent_uploadFile(0,"", SQL_Functions.RefNumber, filename, filepath, filetype, txtComments.Text,"", spnType.SelectedItem.ToString(), spnName.SelectedItem.ToString());
                            
                SQL_Functions.uploadFile(SQL_Functions.RefNumber, filename, filetype, comment, spnType.SelectedItem.ToString());

                Toast.MakeText(Activity, "File:" + filename + " uploaded Sucessfully", ToastLength.Long).Show();

                btnUploadDialog.Text = "File Uploaded";
                btnUploadDialog.Enabled = false;
                //}


            }
            catch (Exception ex)
            {
                new AlertDialog.Builder(Activity).SetTitle("Indigent App Error").SetMessage("Upload failed: " + ex.Message +"\n \nPlease try again later").SetCancelable(true).Show();
                
            }

           


        }

        public void addItemsOnSpinner()
        {

            spnType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>  (SpnType_ItemSelected);
            var adapter1 = ArrayAdapter.CreateFromResource(Android.App.Application.Context, Resource.Array.AppOcc, Android.Resource.Layout.SimpleSpinnerItem);
            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnType.SetBackgroundColor(Android.Graphics.Color.Black);
           
            spnType.Adapter = adapter1;
           
            spnContentType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>  (SpnContentType_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(Android.App.Application.Context, Resource.Array.DocType, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            
            spnContentType.Adapter = adapter2;
            spnContentType.SetBackgroundColor(Android.Graphics.Color.Black);
           
           

         
      
          
            spnName.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>  (SpnName_ItemSelected);
            string[] applicantNames = new string[] { };

            string childItem = SQL_Functions.applicant_main.first_name + " " + SQL_Functions.applicant_main.last_name;
            List<String> list = new List<string>();
            list.Add(childItem);


           
            ArrayAdapter<String> adapterApplicants = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, list);
            
            //Toast.MakeText(Activity, "Applicant Name: " + list[0], ToastLength.Long).Show();
          
            adapterApplicants.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnName.Adapter = adapterApplicants;
            //spnName.SetBackgroundColor(Android.Graphics.Color.Black);
        }

        private void SpnName_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            dName = e.ToString();
        }

        private void SpnContentType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            dType = e.ToString();



        }

        private void SpnType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            

            if (spnType.SelectedItem.ToString().Contains("Applicant"))
            {
                string[] applicantNames = new string[] { SQL_Functions.applicant_main.first_name + " " + SQL_Functions.applicant_main.last_name };
                ArrayAdapter<String> adapterApplicants = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, applicantNames);
                adapterApplicants.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spnName.Adapter = adapterApplicants;
            }
            
            if (spnType.SelectedItem.ToString().Contains("Occupant"))
            {

                if (SQL_Functions.Occupants_list != null)
                {
                    ArrayAdapter<String> adapterOccupant = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem);

                    foreach (SQL_Functions.occupant currentOccupant in SQL_Functions.Occupants_list)
                    {
                        adapterOccupant.Add(currentOccupant.first_name + " " + currentOccupant.last_name);
                    }
                    adapterOccupant.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    spnName.Adapter = adapterOccupant;
                }

                

            }
        }

 
    }
}