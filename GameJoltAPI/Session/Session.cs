using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameJoltAPI.Helpers;
using System.Timers;

namespace GameJoltAPI
{
    /// <summary>
    /// <para>This singleton safe-thread handles sessions. All functions are exposed for manual use, there's also a function for automated handling.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/sessions/ for further information.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public sealed class Session
    {
        private static volatile Session instance;
        private static object syncRoot = new Object();

        private static int game_id;
        private static string username;
        private static string user_token;
        private static SessionStatus status = SessionStatus.Active;
        private static bool automated = false;

        private static Timer pingTimer = new Timer();

        private Session() { }

        /// <summary>
        /// Used to reference the singleton class. We use this as an accessor to the methods within.
        /// </summary>
        public static Session Instance
        {
            get 
            {
                if (instance == null) 
                {
                    lock (syncRoot) 
                    {
                        if (instance == null)
                        {
                            instance = new Session();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// <para>Opens the the session. Will auto-manage, if automated isn't overriden.</para>
        /// <para>Throws an API exception if unsuccesful.</para>
        /// </summary>
        /// <param name="game_id"></param>
        /// <param name="username"></param>
        /// <param name="user_token"></param>
        /// <param name="automated"></param>
        public static void open(int game_id, string username, string user_token, bool automated = true)
        {
            /* Checks if it's succesful or not. User doesn't need the success message, so we won't supply it. */
            lock (syncRoot)
            {
                Keypair.getData("http://gamejolt.com/api/game/v1/sessions/open/?game_id=" + game_id.ToString() + "&username=" + username + "&user_token=" + user_token);
                Session.game_id = game_id;
                Session.username = username;
                Session.user_token = user_token;
                Session.automated = automated;

                if (automated)
                {
                    Timer pingTimer = new Timer();
                    pingTimer.Elapsed += new ElapsedEventHandler(Session.pingE);
                    pingTimer.Interval = 60000; // 1 minute
                    pingTimer.Start();
                }
            }
        }

        /// <summary>
        /// <para>Do not use if automated. Pings the API, to indicate the session. Uses the stored status. changeStatus() to alter this.</para>
        /// <para>Throws an API exception if unsuccesful.</para>
        /// </summary>
        public static void ping()
        {
            lock (syncRoot)
            {
                Keypair.getData("http://gamejolt.com/api/game/v1/sessions/ping/?game_id=" + Session.game_id.ToString() + "&username=" + Session.username + "&user_token=" + Session.user_token + "&status=" + Session.status.ToString());
            }
        }

        /// <summary>
        /// Calls ping, with event parameters as required by timer. Doesn't need to be exposed, thus different accessor.
        /// </summary>
        private static void pingE(object source, ElapsedEventArgs e)
        {
            Session.ping();
        }

        /// <summary>
        /// <para>Closes the session. Call this when the game is closing, or you no longer want sessions.</para> 
        /// <para>Throws an API exception if unsuccesful.</para>
        /// </summary>
        public static void close()
        {
            lock (syncRoot)
            {
                Session.pingTimer.Stop();
                Keypair.getData("http://gamejolt.com/api/game/v1/sessions/close/?game_id=" + Session.game_id.ToString() + "&username=" + Session.username + "&user_token=" + Session.user_token + "&status=" + Session.status.ToString());
            }
        }

        /// <summary>
        /// <para>Changes the session to the one passed. Active by default. Won't update until next ping (at worst 60 seconds delay).</para>
        /// </summary>
        /// <param name="status">SessionStatus enum to indicate the status.</param>
        public static void changeStatus(SessionStatus status)
        {
            Session.status = status;
        }

        /// <summary>
        /// On destruction of this class (when exiting, or whenever session is no longer needed) stop the pings and call the close api.
        /// </summary>
        ~Session()
        {
            Session.close();
        }
    }

    /// <summary>
    /// Enum collection of possible session statuses.
    /// </summary>
    public enum SessionStatus
    {
        Active,
        Idle
    }
}
