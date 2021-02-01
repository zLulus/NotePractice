using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.GetLength
{
    public class GetLengthDemo
    {
        public static void Run()
        {
            var s = "assdf某某某";
            var bytes = Encoding.Default.GetBytes(s);
            //5+3*3=14
            var bytesLength = bytes.Length;
            //8
            var strLength = s.Length;
        }
    }
}
