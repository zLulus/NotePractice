using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Extensions1
{
    public static class StringExtension1
    {
        public static bool IsNullOrEmptyCustomExtension(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
