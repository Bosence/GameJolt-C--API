using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI
{
    public class Score
    {
        private int Sort;
        private string Score;
        private string Extra_data;
        private string User;
        private int userid;
        private string Guest;
        private string Stored;

        /// <summary>
        /// Creates a new GameJoltAPI score instance. Represents the callback information.
        /// </summary>
        /// <param name="sort">The score's numerical sort value.</param>
        /// <param name="score">The score string.</param>
        /// <param name="extra_data">Any extra data associated with the score.</param>
        /// <param name="user">If this is a user score, this is the display name for the user.</param>
        /// <param name="user_id">If this is a user score, this is the user's ID.</param>
        /// <param name="guest">If this is a guest score, this is the guest's submitted name.</param>
        /// <param name="stored">Returns when the score was logged by the user.</param>
        public Score(int sort, string score = null, string extra_data = null, string user = null, int? user_id = null, string guest = null, string stored = null)
        {
            this.Sort = sort;
            this.Score = score;
            this.Extra_data = extra_data;
            this.User = user;
            if (user_id.HasValue) { this.userid = user_id.Value; }
            this.Guest = guest;
            this.Stored = stored;
        }

        /// <summary>
        /// The score's numerical sort value. Returns null if it isn't set.
        /// </summary>
        public int sort
        {
            get { return Sort; }
            set { Sort = value; }
        }

        /// <summary>
        /// The score string. Returns null if it isn't set.
        /// </summary>
        public string score
        {
            get { return Score; }
            set { Score = value; }
        }

        /// <summary>
        /// Any extra data associated with the score. Returns null if it isn't set.
        /// </summary>
        public string extra_data
        {
            get { return Extra_data; }
            set { Extra_data = value; }
        }

        /// <summary>
        /// If this is a user score, this is the display name for the user. Returns null if it isn't set.
        /// </summary>
        public string user
        {
            get { return User; }
            set { User = value; }
        }

        /// <summary>
        /// If this is a user score, this is the user's ID. Returns null if it isn't set.
        /// </summary>
        public int user_id
        {
            get { return userid; }
            set { userid = value; }
        }

        /// <summary>
        /// If this is a guest score, this is the guest's submitted name. Returns null if it isn't set.
        /// </summary>
        public string guest
        {
            get { return Guest; }
            set { Guest = value; }
        }

        /// <summary>
        /// Returns when the score was logged by the user. Returns null if it isn't set.
        /// </summary>
        public string stored
        {
            get { return Stored; }
            set { Stored = value; }
        }

        /// <summary>
        /// Unique instance identifier.
        /// </summary>
        /// <returns>Integer hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
