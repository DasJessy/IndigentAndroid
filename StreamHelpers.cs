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
using System.IO;

namespace IndigentRegister
{
    public static class StreamHelpers
    {

        public static byte[] ReadAllBytess(this BinaryReader reader)
        {
            // Pre .Net version 4.0
            const int bufferSize = 4096;
            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                return ms.ToArray();
            }

            // .Net 4.0 or Newer
            //using (var ms = new MemoryStream())
            //{
            //    Stream.CopyTo(ms);
            //    return ms.ToArray();
            //}
        }

    }
}