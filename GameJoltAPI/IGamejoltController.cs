using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI
{
    /// <summary>
    /// Interface designed to be used for API calls.
    /// It is what will actually make the calls to the API.
    /// The controller is designed to be run Asyncronous.
    /// @author Samuel Wronski
    /// @version 0.1.0.0
    /// </summary>
    interface IGamejoltController
    {
        #region Trophies
        /// <summary>
        /// Set a trophy as achieved for a specific user
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">The username of the user</param>
        /// <param name="token">the user's token</param>
        /// <param name="trophyID">The ID of the trophy to add for the user</param>
        /// <param name="callback">the callback function</param>
        public void AwardTrophy(int gameid, string username, string token, int trophyID, AsyncCallback callback);
        /// <summary>
        /// Returns either one trophy or multiple trophies, depending on the parameters passed in.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">The username of the user</param>
        /// <param name="token">the user's token</param>
        /// <param name="achieved">Set to "true" to return only the achieved trophies for a user or "false" to return only trophies the user hasn't achieved yet. Null to retrieve all trophies.</param>
        /// <param name="trophyID">
        /// If you would like to return just one trophy, you may pass the trophy ID with this parameter. If you do, only that trophy will be returned in the response. 
        /// You may also pass multiple trophy IDs here if you want to return a subset of all the trophies - you do this as a comma separated list in the same way you would retrieving multiple users.
        /// Passing a trophy_id or a set of trophy_ids will ignore the "achieved" parameter if it is passed.
        /// </param>
        /// <param name="callback">The callback function</param>
        public void FetchTrophy(int gameid, string username, string token, bool? achieved = null, int? trophyID = null, AsyncCallback callback);
        #endregion

        #region Users
        /// <summary>
        /// This is used to authenticate the User and should be done before any calls are made for them.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">The Username of the user</param>
        /// <param name="token">The user's token</param>
        /// <param name="callback">the callback function</param>
        public void AuthenticateUser(int gameid, string username, string token, AsyncCallback callback);
        /// <summary>
        /// Fetches a user based on either their User ID or Username.
        /// If neither a User ID or Username is provided an exception should be thrown.
        /// </summary>
        /// <param name="gameid">the ID of the game</param>
        /// <param name="userid">The User ID of the user to fetch.</param>
        /// <param name="username">Username of user to fetch.</param>
        /// <param name="callback">The callback function</param>
        public void FetchUser(int gameid, int? userid = null, string? username = null, AsyncCallback callback);
        #endregion

        #region Sessions
        /// <summary>
        /// Opens a game session for a particular user. Allows you to tell Game Jolt that a user is playing your game. 
        /// You must ping the session to keep it active and you must close it when you're done with it. 
        /// Note that you can only have one open session at a time. If you try to open a new session while one is running, 
        /// the system will close out your current one before opening a new one.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">the username of the user</param>
        /// <param name="token">the user's token</param>
        /// <param name="callback">the callback function</param>
        public void OpenSession(int gameid, string username, string token, AsyncCallback callback);
        /// <summary>
        /// Pings an open session to tell the system that it's still active. If the session hasn't been pinged within 120 seconds, 
        /// the system will close the session and you will have to open another one. It's recommended that you ping every 30 seconds 
        /// or so to keep the system from cleaning up your session. You can also let the system know whether the player is in an 
        /// Active or Idle state within your game through this call.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">The username of the user</param>
        /// <param name="token">The user's token</param>
        /// <param name="status">The user's status. The session starts in an Active state.</param>
        /// <param name="callback">The callback function</param>
        public void PingSession(int gameid, string username, string token, SessionStatus? status, AsyncCallback callback);
        /// <summary>
        /// Close the active session.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">The username of the user</param>
        /// <param name="token">The User's token</param>
        /// <param name="callback">the callback function</param>
        public void CloseSession(int gameid, string username, string token, AsyncCallback callback);
        #endregion

        #region Scores
        /// <summary>
        /// Returns a list of scores either for a user or globally for a game.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">The username of the user</param>
        /// <param name="token">The user's token</param>
        /// <param name="limit">the number of scores to return. Default 10. Attempting to return a negative number of scores or more than 100 will result in an Exception</param>
        /// <param name="tableid">The id of the high score table to fetch from. If left blank the primary table will be used.</param>
        /// <param name="callback">The callback function</param>
        public void FetchScore(int gameid, string? username = null, string? token = null, int? limit = null, int? tableid = null, AsyncCallback callback);
        /// <summary>
        /// Adds a score for a user or guest.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="score">This is a string associated with the score.
        /// <example>"234 Jumps"</example>
        /// </param>
        /// <param name="sort">This is a numerical sorting value associated with the score. All sorting works off of this number.
        /// <example>234</example>
        /// </param>
        /// <param name="username">The username of the user</param>
        /// <param name="token">Provide this if the user is registered with Gamejolt. Otherwise they will be treated as a guest.</param>
        /// <param name="meta">If there's any extra data you want to store, this is where it should go. If will not be shown outside of the API.</param>
        /// <param name="tableid">The id of the high score table to submit the score to. If left blank the primary table will be used.</param>
        /// <param name="callback">the callback function</param>
        public void AddScore(int gameid, string score, int sort, string username, string? token = null, string? meta = null, int? tableid = null, AsyncCallback callback);
        /// <summary>
        /// Retruns a last of high score tables for the game
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="callback">The callback function</param>
        public void ScoreTables(int gameid, AsyncCallback callback);
        #endregion

        #region Data Store
        /// <summary>
        /// The Different Data opperations that may be performed by the UpdateData function.
        /// </summary>
        public enum DataOperation
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Append,
            Prepend
        }

        /// <summary>
        /// Returns data from the Data Store
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="key">The key of the data</param>
        /// <param name="username">The user's username</param>
        /// <param name="token">The user's token</param>
        /// <param name="callback">The callback function</param>
        public void FetchData(int gameid, string key, string? username = null, string? token = null, AsyncCallback callback);
        /// <summary>
        /// Sets data in the Data Store
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="key">The key of the data item to set</param>
        /// <param name="data">The data</param>
        /// <param name="username">The user's username</param>
        /// <param name="token">The user's token</param>
        /// <param name="callback">The callback function</param>
        public void SetData(int gameid, string key, string data, string? username = null, string? token = null, AsyncCallback callback);
        /// <summary>
        /// Updates data in the DataStore
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="key">The key of the data item to set</param>
        /// <param name="operation">The operation to perform on the data.</param>
        /// <param name="value">The value to perform the operation with</param>
        /// <param name="username">The user's username</param>
        /// <param name="token">The user's token</param>
        /// <param name="callback">The callback function</param>
        public void UpdateData(int gameid, string key, DataOperation operation, string value, string? username = null, string? token = null, AsyncCallback callback);
        /// <summary>
        /// Removes data from the Data Store.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="key">The key of the data item to remove</param>
        /// <param name="username">The user's name</param>
        /// <param name="token">The user's token</param>
        /// <param name="callback">The callback function</param>
        public void RemoveData(int gameid, string key, string? username = null, string? token = null, AsyncCallback callback);
        /// <summary>
        /// Returns all the keys in either the game's global data store, or all the keys in a user's data store.
        /// </summary>
        /// <param name="gameid">The ID of the game</param>
        /// <param name="username">The user's username</param>
        /// <param name="token">The user's token</param>
        /// <param name="callback">The callback function</param>
        public void GetKeys(int gameid, string? username = null, string? token = null, AsyncCallback callback);
        #endregion
    }
}
