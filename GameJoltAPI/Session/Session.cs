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
        /// Singleton class.
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
        /// <para>Don't need to set any parameters if you've set up Config.</para>
        /// </summary>
        /// <param name="automated">Do you want Session to set up a timer thread to automate the pinging? Defaults to true.</param>
        /// <param name="game_id">ID of your Game. Only set if you haven't in config. Don't need to set parameter if you've set up Config.</param>
        /// <param name="username">Username of your User. Don't need to set parameter if you've set up Config.</param>
        /// <param name="user_token">User token of your User. Don't need to set parameter if you've set up Config.</param>
        public static void open(bool automated = true, int? game_id = null, string username = null, string user_token = null)
        {
            /* Checks if it's succesful or not. User doesn't need the success message, so we won't supply it. */
            lock (syncRoot)
            {
                if (game_id.HasValue) { Session.game_id = game_id.Value; } else { Session.game_id = Config.game_id; }
                if (username != null) { Session.username = username; } else { Session.username = Config.username; }
                if (user_token != null) { Session.user_token = user_token; } else { Session.user_token = Config.user_token; }
                Session.automated = automated;

                Keypair.getData("http://gamejolt.com/api/game/v1/sessions/open/?game_id=" + Session.game_id + "&username=" + Session.username + "&user_token=" + Session.user_token + "&signature=" + Config.signature);

                if (Session.automated)
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
                Keypair.getData("http://gamejolt.com/api/game/v1/sessions/ping/?game_id=" + Session.game_id.ToString() + "&username=" + Session.username + "&user_token=" + Session.user_token + "&status=" + Session.status.ToString() + "&signature=" + Config.signature);
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
                Keypair.getData("http://gamejolt.com/api/game/v1/sessions/close/?game_id=" + Session.game_id.ToString() + "&username=" + Session.username + "&user_token=" + Session.user_token + "&status=" + Session.status.ToString() + "&signature=" + Config.signature);
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
