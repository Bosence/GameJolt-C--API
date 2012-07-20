using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI.Exceptions
{
    /// <summary>
    /// Custom exception, thrown for when the dumpformat returns failure.
    /// </summary>
    public class DumpFormatFailReturned : System.Exception
    {
        public DumpFormatFailReturned(string message = "Failure was returned from the DataDump. Incorrect URL?") : base(message) { }
    }
}
