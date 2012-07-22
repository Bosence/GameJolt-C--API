using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI.Exceptions
{
    /// <summary>
    /// Custom exception, thrown for when the API returns failure.
    /// </summary>
    public class ConfigNotSet : System.Exception
    {
        public ConfigNotSet(string message = "A variable was not set in Config.") : base(message) { }
    }
}
