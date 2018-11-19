using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Extensions2
{
    public static class StringExtension2
    {
        public static bool IsNullOrEmptyCustomExtension(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
