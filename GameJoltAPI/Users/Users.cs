using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameJoltAPI.Helpers;

namespace GameJoltAPI.Users
{
    /// <summary>
    /// <para>This is a user implementation that is used to authenticate and fetch user information.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/users/ for further information.</para>
    /// <para>@author Sam "runewake2" Wronski</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    class Users
    {
        private static readonly string _fetchURL = @"http://gamejolt.com/api/game/v1/users/";
        private static readonly string _authURL =  @"http://gamejolt.com/api/game/v1/users/auth/";

        private Users() { }

        public static bool AuthenticateUser()
        {
            return AuthenticateUser(Config.username, Config.user_token);
        }

        public static bool AuthenticateUser(string user, string token)
        {
            string request = _authURL + "?game_id=" + Config.game_id + "&signature=" + Config.signature;
            request += "&username=" + Config.username;
            request += "&user_token=" + Config.user_token;
            try
            {
                Dump.getData(request);
            }
            catch (GameJoltAPI.Exceptions.APIFailReturned) { return false; } //If Success was not returned. Failure.
            return true;
        }

        public static User FetchUser(string? username = null, string? user_id = null)
        {
            string request = _authURL + "?game_id=" + Config.game_id + "&signature=" + Config.signature;

            if (username == null && user_id == null)
                username = Config.username;
            if (username != null)
                request += "&username=" + username;
            else request += "&user_id=" + user_id;

            Dictionary<string, string> dic;
            try
            {
                dic = Keypair.getData(request);
            }
            catch (GameJoltAPI.Exceptions.APIFailReturned) { throw new GameJoltAPI.Exceptions.APIFailReturned("Failure was returned, please confirm usename or user id is correct"); }

            try
            {
                int id = Int32.Parse(dic["id"]);
                char tt = dic["type"][0];
                User.UserType type = (tt == 'U'?User.UserType.User:(tt == 'D'?User.UserType.Developer:(tt == 'M'?User.UserType.Moderator:(tt == 'A'?User.UserType.Administrator:User.UserType.User))));
                string usname = dic["username"];
                string avatar_url = dic["avatar_url"];
                string signed_up = dic["signed_up"];
                string last_login = dic["last_logged_in"];
                User.UserStatus status = dic["status"][0] == 'A' ? User.UserStatus.Active : User.UserStatus.Banned;
                string devname = null;
                string devsite = null;
                string devdesc = null;

                //Now for developer only variables
                try
                {
                    if (dic.Keys.Contains("developer_name"))
                        devname = dic["developer_name"];
                    if (dic.Keys.Contains("developer_website"))
                        devsite = dic["developer_website"];
                    if (dic.Keys.Contains("developer_description"))
                        devdesc = dic["developer_description"];
                }
                catch (KeyNotFoundException knfe) { devname = null; devsite = null; devdesc = null; } //If one attribute is not found, revert changes

                User user = new User(id, type, usname, avatar_url, signed_up, last_login, status, devname, devsite, devdesc);
            }
            catch (FormatException fe) { throw new FormatException("The user's data could not be parsed"); }
        }
    }
}
