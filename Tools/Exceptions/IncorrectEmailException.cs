using System;
using System.Collections.Generic;
using System.Text;

namespace _01LabShevchenko.Tools.Exceptions
{
    class IncorrectEmailException:Exception
    {
        public IncorrectEmailException(string message) : base(message)
        {
        }
    }
}
