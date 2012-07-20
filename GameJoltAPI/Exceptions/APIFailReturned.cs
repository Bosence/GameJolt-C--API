using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI.Exceptions
{
    /// <summary>
    /// Custom exception, thrown for when the API returns failure.
    /// </summary>
    public class APIFailReturned : System.Exception
    {
        public APIFailReturned(string message = "Failure was returned from the API. Incorrect URL?") : base(message) { }
    }
}
