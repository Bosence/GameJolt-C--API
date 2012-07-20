using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameJoltAPI.Exceptions;

namespace GameJoltAPI
{
    /// <summary>
    /// <para>Class representing the User API callback information.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/users/fetch/ for further information.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public class User
    {
        /// <summary>
        /// Enum collection of possible account types.
        /// </summary>
        public enum UserType { User, Developer, Moderator, Administrator };
        /// <summary>
        /// Enum collection of possible account statuses.
        /// </summary>
        public enum UserStatus { Active, Banned };

        /* Formating is off, because I've used the API naming schema for the encaps. */
        private int id;
        private UserType Type;
        private string Username;
        // Convert to URI format: http://msdn.microsoft.com/en-us/library/system.uri.aspx ?
        private string Avatar_url;
        private string Signed_up;
        private string Last_logged_in;
        private UserStatus Status;

        /* If the user is a developer */
        private string developerName;
        private string developerWebsite;
        private string developerDescription;

        /// <summary>
        /// Creates a new GameJoltAPI user instance. Represents the callback information.
        /// </summary>
        /// <param name="ID">The ID of the user.</param>
        /// <param name="type">Can be "User", "Developer", "Moderator", or "Administrator". Uses UserType enum.</param>
        /// <param name="username">The user's username.</param>
        /// <param name="avatar_url">The URL of the user's avatar.</param>
        /// <param name="signed_up">How long ago the user signed up.</param>
        /// <param name="last_logged_in">How long ago the user was last logged in. Will be "Online Now" if the user is currently online.</param>
        /// <param name="status">"Active" if the user is still a member on the site. "Banned" if they've been banned. Uses UserStatus enum.</param>
        /// <param name="developerName">The developer's name.</param>
        /// <param name="developerWebsite">The developer's website, if they put one in.</param>
        /// <param name="developerDescription">The description that the developer put in for themselves. HTML tags and new lines have been removed.</param>
        public User(int ID, UserType? type = null, string username = null, string avatar_url = null, string signed_up = null, string last_logged_in = null, UserStatus? status = null,
            string developerName = null, string developerWebsite = null, string developerDescription = null)
        {
            this.id = ID;
            if (type.HasValue) { this.type = type.Value; }
            this.Username = username;
            this.Avatar_url = avatar_url;
            this.Signed_up = signed_up;
            this.Last_logged_in = last_logged_in;
            if (status.HasValue) { this.Status = status.Value; }

            this.developerName = developerName;
            this.developerWebsite = developerWebsite;
            this.developerDescription = developerDescription;
        }

        /// <summary>
        /// The ID of the user. Returns null if it isn't set.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Can be "User", "Developer", "Moderator", or "Administrator". Uses UserType enum. Returns null if it isn't set.
        /// </summary>
        public UserType? type
        {
            get { return this.Type; }
            set { this.Type = value.Value; }
        }

        /// <summary>
        /// The user's username. Returns null if it isn't set.
        /// </summary>
        public string username
        {
            get { return this.Username; }
            set { this.Username = value; }
        }

        /// <summary>
        /// The URL of the user's avatar. Returns null if it isn't set.
        /// </summary>
        public string avatar_url
        {
            get { return this.Avatar_url; }
            set { this.Avatar_url = value; }
        }

        /// <summary>
        /// How long ago the user signed up. Returns null if it isn't set.
        /// </summary>
        public string signed_up
        {
            get { return this.Signed_up; }
            set { this.Signed_up = value; }
        }

        /// <summary>
        /// How long ago the user was last logged in. Will be "Online Now" if the user is currently online. Returns null if it isn't set.
        /// </summary>
        public string last_logged_in
        {
            get { return this.Last_logged_in; }
            set { this.Last_logged_in = value; }
        }

        /// <summary>
        /// "Active" if the user is still a member on the site. "Banned" if they've been banned. Uses UserStatus enum. Returns null if it isn't set.
        /// </summary>
        public UserStatus? status
        {
            get { return this.Status; }
            set { this.Status = value.Value; }
        }

        /// <summary>
        /// <para>Get or set the users developer_name. Returns null if it isn't set.</para>
        /// <para>Throws a UserNotDeveloper exception if the user doesn't have the sufficent Usertype.</para>
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

        /// <summary>
        /// <para>Get or set the developers website. Returns null if it isn't set.</para>
        /// <para>Throws a UserNotDeveloper exception if the user doesn't have the sufficent Usertype.</para>
        /// </summary>
        public string developer_website
        {
            get
            {
                if (this.type == UserType.Developer)
                {
                    return developerWebsite;
                }
                else
                {
                    throw new UserNotDeveloper();
                }
            }
            set { developerWebsite = value; }
        }

        /// <summary>
        /// <para>Get or set the developers description. Returns null if it isn't set.</para>
        /// <para>Throws a UserNotDeveloper exception if the user doesn't have the sufficent Usertype.</para>
        /// </summary>
        public string developer_description
        {
            get {
                if (this.type == UserType.Developer)
                {
                    return developerDescription;
                }
                else
                {
                    throw new UserNotDeveloper();
                }
            }
            set { developerDescription = value; }
        }
    }
}
