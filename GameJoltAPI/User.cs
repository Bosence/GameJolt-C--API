using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI
{
    /// <summary>
    /// Class representing the User API callback information.
    /// See: http://gamejolt.com/api/doc/game/users/fetch/ for further information.
    /// @author Christian Bosence
    /// @version 0.1.0.0
    /// </summary>
    public class User
    {
        /// <summary>
        /// Enum collection of possible account types.
        /// </summary>
        public static enum UserType { User, Developer, Moderator, Administrator };
        /// <summary>
        /// Enum collection of possible account statuses.
        /// </summary>
        public static enum UserStatus { Active, Banned };

        private int ID;
        private UserType type;
        private string username;
        // Convert to URI format: http://msdn.microsoft.com/en-us/library/system.uri.aspx ?
        private string avatar_url;
        private string signed_up;
        private string last_logged_in;
        private UserStatus status;

        /* If the user is a developer */
        private string developerName;
        private string developerWebsite;
        private string developerDescription;

        /// <summary>
        /// Creates a new GameJoltAPI user instance.
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <param name="type">UserType</param>
        /// <param name="username">Username</param>
        /// <param name="avatar_url">Avatar Url</param>
        /// <param name="signed_up">Signed Up</param>
        /// <param name="last_logged_in">Last Logged In</param>
        /// <param name="status">UserStatus</param>
        public User(int? ID = null, UserType? type = null, string username = null, string avatar_url = null, string signed_up = null, string last_logged_in = null, UserStatus? status = null)
        {
            // This all needs a clean up. Hastily done. General idea.
            if (ID.HasValue) { this.ID = ID.Value; }
            if (type.HasValue) { this.type = type.Value; }
            if (username != null) { this.username = username; }
            if (avatar_url != null) { this.avatar_url = avatar_url; }
            if (signed_up != null) { this.signed_up = signed_up; }
            if (last_logged_in != null) { this.last_logged_in = last_logged_in; }
            if (status.HasValue) { this.status = status.Value; }
        }

        /// <summary>
        /// Get or set the users developer_name.
        /// Throws a UserNotDeveloper exception if the user doesn't have the sufficent Usertype.
        /// </summary>
        public string developer_name
        {
            get {
                if (this.type == UserType.Developer)
                {
                    return developerName;
                }
                else
                {
                    throw new UserNotDeveloper();
                }
            }
            set { developerName = value; }
        }
    }

    /// <summary>
    /// Custom exception, thrown when user is not developer.
    /// </summary>
    public class UserNotDeveloper : System.Exception
    {
        public UserNotDeveloper() : base("The users type does not match the developer type.") { }
    }
}
