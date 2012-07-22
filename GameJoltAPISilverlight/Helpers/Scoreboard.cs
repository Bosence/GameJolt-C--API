using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace GameJoltAPI.Helpers
{
    class Scoreboard
    {
        /// <summary>
        /// An array of the scores in the scoreboard
        /// </summary>
        [JsonProperty("")]
        ScoreboardScore[] Scores { get; set; }

        public int Count { get { return Scores.Count(); } }
        public ScoreboardScore First { get { return Scores.First(); } }
        public ScoreboardScore Last { get { return Scores.Last(); } }

        /// <summary>
        /// Fetchs a scoreboard from the requested URL
        /// </summary>
        /// <param name="requesturl">the URL request string with which to fetch to scoreboard</param>
        /// <returns></returns>
        public static Scoreboard Fetch(string requesturl)
        {
            Uri u = new Uri(requesturl+"&format=json"); //convert format to JSON
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(u);
            IAsyncResult res = req.BeginGetRequestStream(null, null);
            //while (!res.IsCompleted) ;
            Stream st = req.EndGetRequestStream(res);
            return FromJSON(st);
        }

        /// <summary>
        /// Converts a stream of data in JSON format to a scoreboard
        /// </summary>
        /// <param name="json">The data to convert</param>
        /// <returns></returns>
        private static Scoreboard FromJSON(Stream json)
        {
            JsonReader reader = new JsonTextReader(new StreamReader(json));
            JsonSerializer jss = new JsonSerializer();
            return jss.Deserialize<Scoreboard>(reader);
        }

        /// <summary>
        /// Converts a JSON string to a scoreboard
        /// </summary>
        /// <param name="json">The string of JSON data</param>
        /// <returns></returns>
        private static Scoreboard FromJSON(String json)
        {
            JsonReader reader = new JsonTextReader(new StreamReader(json));
            JsonSerializer jss = new JsonSerializer();
            return jss.Deserialize<Scoreboard>(reader);
        }
    }

    /// <summary>
    /// Helper Class used for storing data in a scoreboard
    /// It is super simple and designed to provide only the information the API provides
    /// </summary>
    class ScoreboardScore
    {
        /// <summary>
        /// The score the player earned
        /// <example>34 Coins Collected</example>
        /// </summary>
        [JsonProperty("score")]
        string Score { get; set; }

        /// <summary>
        /// An integer used to sort a games scores
        /// <example>34</example>
        /// </summary>
        [JsonProperty("sort")]
        int Sort { get; set; }

        /// <summary>
        /// The Extra Data attached to a score
        /// </summary>
        [JsonProperty("extra_data")]
        string Meta { get; set; }

        /// <summary>
        /// The user who's score this is
        /// </summary>
        [JsonProperty("user")]
        string User { get; set; }

        /// <summary>
        /// The ID of the User
        /// </summary>
        [JsonProperty("user_id")]
        string UserID { get; set; }

        //The name of the user if they are a guest
        [JsonProperty("guest")]
        string Guest { get; set; }

        /// <summary>
        /// The time when this was stored
        /// </summary>
        [JsonProperty("stored")]
        string Stored { get; set; }
    }
}
