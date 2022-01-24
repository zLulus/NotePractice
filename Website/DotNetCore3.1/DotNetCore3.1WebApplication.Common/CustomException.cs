using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore3._1WebApplication.Common
{
    public class CustomException: Exception
    {
        public CustomException():base()
        {

        }

        public CustomException(string message) : base(message)
        {

        }
    }
}
