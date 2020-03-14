using System;
using System.Collections.Generic;
using System.Text;

namespace _01LabShevchenko.Tools.Exceptions
{
    class TooOldException:Exception
    {
        public TooOldException(string message):base(message)
        {
        }
    }
}
