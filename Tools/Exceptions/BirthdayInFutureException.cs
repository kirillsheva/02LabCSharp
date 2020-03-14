using System;
using System.Collections.Generic;
using System.Text;

namespace _01LabShevchenko.Tools.Exceptions
{
    class BirthdayInFutureException:Exception
    {
        public BirthdayInFutureException(string message) : base(message)
        {
        }
    }
}
