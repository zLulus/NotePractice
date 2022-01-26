using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Core.Common
{
    public class CustomException : Exception
    {
        public CustomException() : base()
        {

        }

        public CustomException(string message) : base(message)
        {

        }
    }
}
