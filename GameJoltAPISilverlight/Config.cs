using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JeffWilcox.Utilities.Silverlight;
using GameJoltAPI.Exceptions;

namespace GameJoltAPI
{
    /// <summary>
    /// <para>This singleton safe-thread handles Config. You can set the variables here, so you don't need to pass them in functions later.</para>
    /// <para>They will throw ConfigNotSet exception, if accessed when it hasn't been set. Easier to debug than null pointers.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public sealed class Config
    {
        private static volatile Config instance;
        private static object syncRoot = new Object();

        private static string Signature;
        private static int Game_id;
        private static string Username;
        private static string User_token;

        private Config() { }

        /// <summary>
        /// Singleton class.
        /// </summary>
        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Config();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Private Key of your game (found under "Manage Achievements" on the game dashboard). Stores the hash used for API.
        /// </summary>
        public static string signature
        {
            get {
                if (Config.Signature == null)
                {
                    throw new ConfigNotSet("signature was accessed, but has not been set.");
                }
                else
                {
                    return Config.Signature;
                }
            }
            set { Config.Signature = MD5.Create(value).ToString(); }
        }

        /// <summary>
        /// The ID of your game.
        /// </summary>
        public static int game_id
        {
            get {
                if (Config.Game_id == null)
                {
                    throw new ConfigNotSet("game_id was accessed, but has not been set.");
                }
                else
                {
                    return Config.Game_id;
                }
            }
            set { Config.Game_id = value; }
        }

        /// <summary>
        /// The users username.
        /// </summary>
        public static string username
        {
            get
            {
                if (Config.Username == null)
                {
                    throw new ConfigNotSet("username was accessed, but has not been set.");
                }
                else
                {
                    return Config.Username;
                }
            }
            set { Config.Username = value; }
        }

        /// <summary>
        /// The users token.
        /// </summary>
        public static string user_token
        {
            get { 
                if (Config.User_token == null) {
                    throw new ConfigNotSet("user_token was accessed, but has not been set.");
                } else {
                    return Config.User_token;
                }
            }
            set { Config.User_token = value; }
        }
    }
}
