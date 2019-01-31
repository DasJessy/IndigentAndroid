using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using System.Data.SqlClient;
using System.Collections;
using Android.Provider;
using Android.Database;
using Java.IO;

namespace IndigentRegister
{
    public static class SQL_Functions
    {

        public struct applicant
        {
            public string first_name;
            public string last_name;
            public string Id_number;
            public string account_number;
            public string INDRefno;
            public string[] Filename;
           
        }

        public struct occupant
        {
            public string first_name;
            public string last_name;
            public string Id_number;
        }

        public struct indigentFileStruct
        {
            public string fullname;
            public string contentType;
                public string Filename;
        }

        public static applicant applicant_main = new applicant();
        public static occupant occupants = new occupant();
        public static ArrayList Occupants_list = new ArrayList();
        
        public static string RefNumber;
        public static string Name;
        public static string errorMessage = string.Empty;
        public static string info2 = "";
        public static byte[] fileByte = default(byte[]);


        public static bool intialiseApplicant()
        {
            
            string con_string = GetConnectionstring();
           
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    SqlDataReader datareader; 
                    conn.ConnectionString = con_string;

                    SqlCommand command = new SqlCommand("SELECT FirstName,LastName,IDNumber,AccountNumber FROM Indigents WHERE AccountNumber=" + RefNumber.ToString() + ";");

                    command.CommandType = CommandType.Text; 
                    command.Connection = conn;
                    conn.Open();
                    command.ExecuteNonQuery();
                    datareader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    if (datareader.HasRows)
                    {
                        while (datareader.Read())
                        {
                            applicant_main.first_name = datareader["FirstName"].ToString();
                            applicant_main.last_name = datareader["LastName"].ToString();
                            applicant_main.Id_number = datareader["IDNumber"].ToString();
                            applicant_main.account_number = datareader["AccountNumber"].ToString();
                            

                        }
                    }
                    conn.Close();
                   
                }
                errorMessage = string.Empty;
               // Toast.MakeText(Android.App.Application.Context, "Applicants Initialised", ToastLength.Short).Show();
               
                return true;

            }
           catch( Exception ex)
            {
                errorMessage = "Reference Number: "+ RefNumber.ToString() + " "+ ex.Message + ex.StackTrace;
                Toast.MakeText(Android.App.Application.Context, errorMessage, ToastLength.Long).Show();
                return false;
            }
                

            
        }

        public static bool intialiseOccupant()
        {
            string con_string = GetConnectionstring();

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    SqlDataReader datareader;
                    conn.ConnectionString = con_string;
                    SqlCommand command = new SqlCommand("SELECT FirstName,LastName,IDNumber FROM IndegentOccupantDetails WHERE AccNo=" + applicant_main.account_number.ToString()+";");
                    command.CommandType = CommandType.Text;
                    command.Connection = conn;
                    conn.Open();
                    command.ExecuteNonQuery();
                    datareader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    if (datareader.HasRows)
                    {
                        while (datareader.Read())
                        {
                            occupants.first_name = datareader["FirstName"].ToString();
                            occupants.last_name = datareader["LastName"].ToString();
                            occupants.Id_number = datareader["IDNumber"].ToString();
                           
                            Occupants_list.Add(occupants);
                             
                        }
                    }
                    else
                    {
                        
                    }
                    conn.Close(); 
                }
                return true;
            }
            catch (Exception ex)
            {
               

                errorMessage += "Initialise Occupant(s): " + ex.Message+ ex.StackTrace;
                Toast.MakeText(Android.App.Application.Context, errorMessage, ToastLength.Long).Show();
                return false;
            }

            
        }

        public static string GetConnectionstring()
        {

            return "Data Source = winsql01.hkdns.co.za,1433; Initial Catalog = DevRegister_sql; Persist Security Info = True; User ID = DEVAdmin_sql; Password =P@ssw0rd14";
        }

        //public static void getFileList(int id)
        //{
        //    string con_string = GetConnectionstring();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection())
        //        {
        //            SqlDataReader datareader;
        //            conn.ConnectionString = con_string;

        //            SqlCommand command = new SqlCommand("SELECT FullName, Name,ContentType FROM Files WHERE AccNo=" + id.ToString() + ";");

        //            command.CommandType = CommandType.Text;
        //            command.Connection = conn;
        //            conn.Open();
        //            command.ExecuteNonQuery();
        //            datareader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //            if (datareader.HasRows)
        //            {
        //                while (datareader.Read())
        //                {
        //                    applicant_main.first_name = datareader["FirstName"].ToString();
        //                    applicant_main.last_name = datareader["LastName"].ToString();
        //                    applicant_main.Id_number = datareader["IDNumber"].ToString();
        //                    applicant_main.account_number = datareader["AccountNumber"].ToString();





        //                }
        //            }
        //            conn.Close();

        //        }
        //        errorMessage = string.Empty;
        //        // Toast.MakeText(Android.App.Application.Context, "Applicants Initialised", ToastLength.Short).Show();

        //        return true;



        //    }

        //    }

        //public static byte[] returnBytes()
        //{

        //   // byte[] secondBytes =System.IO.File.ReadAllBytes(DialogUpload.filepath);

        //    //using (var streamReader = new StreamReader(DialogUpload.filepath))
        //    //{
        //    //    Stream fs = streamReader.BaseStream;
        //    //    BinaryReader br = new BinaryReader(fs);

        //    //    Byte[] bytes = br.ReadBytes(fs.Length);
        //    //    using (var memstream = new MemoryStream())
        //    //    {
        //    //        streamReader.BaseStream.CopyTo(memstream);
        //    //        bytes = memstream.ReadByte.;
        //    //        return bytes;

        //    //    }
        //    //}


        //    return secondBytes;
        //}

        ////public static byte[] readallbyte()
        //{

        //    byte[] buffer = File.ReadAllBytes(DialogUpload.filepath);
        //    Stream fs = new MemoryStream(buffer);
        //    BinaryReader br = new BinaryReader(fs);
            
        //    byte[] bytes = br.ReadBytes((int)fs.Length);
   

        //    return buffer;

        //}

        public static Byte [] readbytes()
        {
            //FileStream stream = File.OpenRead(DialogUpload.filepath);
            //byte[] fileBytes = BitConverter.GetBytes(stream.Length);
            //stream.Read(fileBytes, 0, fileBytes.Length);
            //stream.Close();
            //return fileBytes;


            //using (var streamReader = new StreamReader(DialogUpload.filepath))
            //{
            //    var bytes = default(byte[]);
            //    using (var memstream = new MemoryStream())
            //    {
            //        streamReader.BaseStream.CopyTo(memstream);
            //        bytes = memstream.ToArray();
            //        return bytes;
            //    }

            //}

            //-> DOes not work
            //byte[] file = System.IO.File.ReadAllBytes(DialogUpload.filepath);


            Java.IO.RandomAccessFile f = new Java.IO.RandomAccessFile(new Java.IO.File(DialogUpload.filepath), "r");

            byte[] b = new byte[(long)f.Length()];

            f.ReadFully(b);

            return b;


        }
    
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            const int bufferSize = 4096;
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                return ms.ToArray();
            }

        }

        public static bool uploadFile(string RefNumber, string filename, string contentType, string comments, string IndigentAPPMemberType)
        {


            System.Net.ServicePointManager.Expect100Continue = false;
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3| System.Net.SecurityProtocolType.Tls;
                string con_string = GetConnectionstring();

            //FileStream fs = new FileStream(DialogUpload.filepath, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);

            //byte[] dataToUpload = StreamHelpers.ReadAllBytes(br);
            //byte[] dataToUpload = br.ReadAllBytes();
            //var bytes = default(byte[]);
            //using (var streamReader = new StreamReader(DialogUpload.filepath))
            //{

            //    using (var memstream = new MemoryStream())
            //    {
            //        streamReader.BaseStream.CopyTo(memstream);
            //        bytes = memstream.ToArray();
            //    }
            //}


            //WcfService_Indigent_Final.Service1 myservice = new WcfService_Indigent_Final.Service1();
           // myservice.Indigent_uploadFile()

            

            using (SqlConnection conn = new SqlConnection())
                {
                // byte[] bytes_t = new byte[1]; 
                conn.ConnectionString = con_string;
                SqlCommand command = new SqlCommand("INSERT INTO Files (INDRefno,Name,ContentType,Data,APPIDnumber,AccNO,IndigentAPPMemberType,FullName,IDnumber,DocumentComments) values(@INDRefno,@Name,@ContentType,@Data,@APPIDnumber,@AccNO,@IndigentAPPMemberType,@FullName,@IDnumber,@DocumentComments)");
                //SqlCommand command = new SqlCommand("INSERT INTO Files (INDRefno,Name,ContentType,APPIDnumber,AccNO,IndigentAPPMemberType,FullName,IDnumber,DocumentComments) values(@INDRefno,@Name,@ContentType,@APPIDnumber,@AccNO,@IndigentAPPMemberType,@FullName,@IDnumber,@DocumentComments)");
                command.Connection = conn;
                command.Parameters.AddWithValue("@INDRefno", RefNumber);
                command.Parameters.AddWithValue("@Name", filename.ToString());
                command.Parameters.AddWithValue("@ContentType", contentType.ToString());
               
                    command.Parameters.AddWithValue("@Data", readbytes());
                
                command.Parameters.AddWithValue("@APPIDnumber", applicant_main.Id_number);
                command.Parameters.AddWithValue("@AccNO", applicant_main.account_number.ToString());
                command.Parameters.AddWithValue("@IndigentAPPMemberType", IndigentAPPMemberType);
                
                command.Parameters.AddWithValue("@FullName", applicant_main.first_name.ToString() + applicant_main.last_name.ToString());
                command.Parameters.AddWithValue("@IDnumber", applicant_main.Id_number);
                command.Parameters.AddWithValue("@DocumentComments", comments.ToString());
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
              
                }
            
            
            return true;
        }

        public static bool uploadSignature(byte[] signatureArray)
        {

            System.Net.ServicePointManager.Expect100Continue = false;
            string con_string = GetConnectionstring();
          

            using (SqlConnection conn = new SqlConnection())
            {
            
                conn.ConnectionString = con_string;
                SqlCommand command = new SqlCommand("UPDATE Indigents SET Signature = @Sign WHERE AccountNumber =" + applicant_main.account_number.ToString() + ";");
                command.Connection = conn;
                command.Parameters.AddWithValue("@Sign", signatureArray);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

            }

            return true;
        }

        public static bool downloadFile(string fileid)
        {
            fileByte = null;
            var filename = "";
            System.Net.ServicePointManager.Expect100Continue = false;
            string con_string = GetConnectionstring();
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

            var downloadLoc = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads) + "'\'" + filename ;
            Java.IO.File outputFile = new Java.IO.File(downloadLoc);

            try
            {
                if (outputFile.Exists())
                {
                    outputFile.CreateNewFile();
                }
                FileOutputStream outputStream = new FileOutputStream(outputFile);
                outputStream.Write(fileByte);  //write the bytes and im done. 

            } catch (Exception ex)
            {
                new AlertDialog.Builder(Android.App.Application.Context).SetTitle("Indigent App Error").SetMessage("Upload failed: " + ex.Message + "\n \nPlease try again later").SetCancelable(true).Show();
            }



            return true;

        }

    }
}