using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CodeLibraryForDotNetCore.StringConverters
{
    public class StringDemo
    {
        public static void Run()
        {
            //string to byte[]
            string s = "your byte";
            byte[] bs = new byte[s.Length];
            var c=s.ToCharArray();
            for(int i=0;i<c.Length;i++)
            {
                bs[i] = (byte)c[i];
            }
            //byte[] to string
            string s0 = System.Text.Encoding.UTF8.GetString(bs);
           
        }
    }
}
