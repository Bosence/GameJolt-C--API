using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJoltAPI
{
    /// <summary>
    /// <para>Class representing the Trophy API callback information.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/trophy/fetch/ for further information.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public class Trophy
    {
        /// <summary>
        /// Enum collection of possible difficulty levels.
        /// </summary>
        public enum TrophyDifficulty { Bronze, Silver, Gold, Platinum };

        private int id;
        private string Title;
        private string Description;
        private TrophyDifficulty Difficulty;
        // Convert to URI format: http://msdn.microsoft.com/en-us/library/system.uri.aspx ?
        private string imageURL;
        private string Achieved;

        /// <summary>
        /// Creates a new GameJoltAPI trophy instance. Represents the callback information.
        /// </summary>
        /// <param name="ID">The ID of the trophy.</param>
        /// <param name="title">The title of the trophy on the site.</param>
        /// <param name="description">The trophy description text.</param>
        /// <param name="difficulty">"Bronze", "Silver", "Gold" or "Platinum". Uses TrophyDifficulty enum.</param>
        /// <param name="image_url">The URL to the trophy's thumbnail.</param>
        /// <param name="achieved">Returns when the trophy was achieved by the user, or "false" if they haven't achieved it yet.</param>
        public Trophy(int ID, string title = null, string description = null, TrophyDifficulty? difficulty = null, string image_url = null, string achieved = null)
        {
            this.id = ID;
            this.Title = title;
            this.Description = description;
            if (difficulty.HasValue) { this.Difficulty = difficulty.Value; }
            this.imageURL = image_url;
            this.Achieved = achieved;
        }

        /// <summary>
        /// The ID of the trophy. Returns null if it isn't set.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        /// <summary>
        /// The title of the trophy on the site. Returns null if it isn't set.
        /// </summary>
        public string title
        {
            get { return Title; }
            set { Title = value; }
        }

        /// <summary>
        /// The trophy description text. Returns null if it isn't set.
        /// </summary>
        public string description
        {
            get { return Description; }
            set { Description = value; }
        }

        /// <summary>
        /// "Bronze", "Silver", "Gold" or "Platinum". Uses TrophyDifficulty enum. Returns null if it isn't set.
        /// </summary>
        public TrophyDifficulty difficulty
        {
            get { return Difficulty; }
            set { Difficulty = value; }
        }

        /// <summary>
        /// The URL to the trophy's thumbnail. Returns null if it isn't set.
        /// </summary>
        public string image_url
        {
            get { return imageURL; }
            set { imageURL = value; }
        }

        /// <summary>
        /// Returns when the trophy was achieved by the user, or "false" if they haven't achieved it yet. Returns null if it isn't set.
        /// </summary>
        public string achieved
        {
            get { return Achieved; }
            set { Achieved = value; }
        }
    }
}
