using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI
{
    /// <summary>
    /// <para>Class representing the Score API callback information.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/scores/fetch/ for further information.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public class Score
    {
        private int Sort;
        private string value;
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
            this.value = score;
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
            get { return value; }
            set { this.value = value; }
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

    /// <summary>
    /// <para>Class representing the scoretable API callback information.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/scores/fetch/ for further information.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public class ScoreTable
    {
        private int ID;
        private string Name;
        private string Description;
        private bool Primary;
        
        /// <summary>
        /// Creates a new GameJoltAPI scoretable instance. Represents the callback information.
        /// </summary>
        /// <param name="id">The high score table identifier.</param>
        /// <param name="name">The developer-defined high score table name.</param>
        /// <param name="description">The developer-defined high score table description.</param>
        /// <param name="primary">Whether or not this is the default high score table. High scores are submitted to the primary table by default.</param>
        public ScoreTable(int id, string name = null, string description = null, bool primary = true)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Primary = primary;
        }

        /// <summary>
        /// The high score table identifier. Returns null if it isn't set.
        /// </summary>
        public int id
        {
            get { return ID; }
            set { ID = value; }
        }

        /// <summary>
        /// The developer-defined high score table name. Returns null if it isn't set.
        /// </summary>
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        /// <summary>
        /// The developer-defined high score table description. Returns null if it isn't set.
        /// </summary>
        public string description
        {
            get { return Description; }
            set { Description = value; }
        }

        /// <summary>
        /// Whether or not this is the default high score table. High scores are submitted to the primary table by default. Returns null if it isn't set.
        /// </summary>
        public bool primary
        {
            get { return Primary; }
            set { Primary = value; }
        }
    }
}
