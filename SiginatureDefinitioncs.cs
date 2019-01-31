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
using SignaturePad;
using Android.Support.V7.App;
using Java.IO;
using Java.Nio;
using System.IO;
using Android.Graphics;

namespace IndigentRegister
{
    
    public class SiginatureDefinitioncs : DialogFragment
    {

        public static Android.Graphics.Bitmap SignatureImage;
        SignaturePadView sign_pad;
        LinearLayout signaturePlaceHolder;
        Button btnSave, btnClear, btnQuit;

        public void initialiseDialog()
        {

            sign_pad = new SignaturePadView(Activity)
            {
                StrokeWidth = 3F,
                StrokeColor = Android.Graphics.Color.Black,
                SignatureLineColor = Android.Graphics.Color.Black,
                BackgroundColor = Android.Graphics.Color.White


            };
            sign_pad.SignaturePrompt.Text = "✍";
            sign_pad.SignaturePrompt.SetTextColor(Android.Graphics.Color.Black);
            sign_pad.Caption.SetTextColor(Android.Graphics.Color.Black);
            sign_pad.Caption.Text = "Please Sign Here";

           
            

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Sign_inflater, container, false); ;

            return view;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {

            Dialog dialog = base.OnCreateDialog(savedInstanceState);

            dialog.SetTitle("Indigent(s) Signature");

            return dialog;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            
            base.OnViewCreated(view, savedInstanceState);

            initialiseDialog();
            signaturePlaceHolder = View.FindViewById<LinearLayout>(Resource.Id.lnrSignaturePlaceHolder);
            btnSave = view.FindViewById<Button>(Resource.Id.btnSaveSignature);
            btnClear = view.FindViewById<Button>(Resource.Id.btnClearSignature);
            btnQuit = view.FindViewById<Button>(Resource.Id.btnGoBackSignature);
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
            btnQuit.Click += BtnQuit_Click;
            
            signaturePlaceHolder.AddView(sign_pad);
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            sign_pad.Clear();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SignatureImage = sign_pad.GetImage();

           


            //byte[] b = new byte[(long)f.Length()];

            //f.ReadFully(b);

            //Java.IO.File file;


            //Toast.MakeText(Activity, "Signature Saved", ToastLength.Short).Show();
            //ByteBuffer buffer = ByteBuffer.Allocate(SignatureImage.ByteCount);
            //SignatureImage.CopyPixelsToBuffer(buffer);
            //buffer.Rewind();



            ////Byte array signature
            //IntPtr classHandle = JNIEnv.FindClass("java/nio/ByteBuffer");
            //IntPtr methodId = JNIEnv.GetMethodID(classHandle, "array", "()[B");
            //IntPtr resultHandle = JNIEnv.CallObjectMethod(buffer.Handle, methodId);
            //byte[] result = JNIEnv.GetArray<byte>(resultHandle);
            //JNIEnv.DeleteLocalRef(resultHandle);

            try
            {

                int byteSize = SignatureImage.RowBytes * SignatureImage.Height;
                int bytes = SignatureImage.ByteCount;

                MemoryStream stream = new MemoryStream();
                SignatureImage.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();
              

                bool uploadsign = SQL_Functions.uploadSignature(bitmapData);
                Toast.MakeText(Activity, "Signature Uploaded", ToastLength.Short).Show();
            }
            catch(Exception ex)
            {
                new Android.Support.V7.App.AlertDialog.Builder(Activity).SetTitle("Indigent App Error").SetMessage("Upload failed: " + ex.Message + "\n \nPlease try again later").SetCancelable(true).Show();
            }


            Dismiss();


        }
    }
}