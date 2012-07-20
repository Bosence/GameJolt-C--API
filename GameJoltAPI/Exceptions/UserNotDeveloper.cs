using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI.Exceptions
{
    /// <summary>
    /// Custom exception, thrown when user is not developer.
    /// </summary>
    public class UserNotDeveloper : System.Exception
    {
        public UserNotDeveloper() : base("The users type does not match the developer type.") { }
    }
}
