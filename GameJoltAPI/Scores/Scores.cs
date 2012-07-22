using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GameJoltAPI.Helpers;

namespace GameJoltAPI.Scores
{
    /// <summary>
    /// <para>This is a score implementation to be used for retrieving and saving scores</para>
    /// <para>See: http://gamejolt.com/api/doc/game/scores/ for further information.</para>
    /// <para>@author Sam "runewake2" Wronski</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    class Scores
    {
        private static readonly string _fetchURL = @"http://gamejolt.com/api/game/v1/scores/";
        private static readonly string _addURL = @"http://gamejolt.com/api/game/v1/scores/add/";
        private static readonly string _tablesURL = @"http://gamejolt.com/api/game/v1/scores/tables/";

        /// <summary>
        /// Creates a new Scores instance that will use Config's data
        /// </summary>
        public Scores() { }

        public Scoreboard Fetch(int? limit = null, int? tableid = null)
        {
            string request = _fetchURL + "?game_id=" + Config.game_id + "&signature=" + Config.signature;
            request += "&username=" + Config.username;
            request += "&user_token=" + Config.user_token;
            if (limit != null)
                request += "&limit=" + limit;
            if (tableid != null)
                request += "&table_id=" + tableid;
            //done building URL
            return Scoreboard.Fetch(request);
        }
    }
}
