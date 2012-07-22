using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using GameJoltAPI.Exceptions;

namespace GameJoltAPI.Helpers
{
    /// <summary>
    /// <para>Provides a list of functions used to manipulate the keypair format the API provides.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/formats/keypair/ for further information.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public static class Keypair
    {
        /// <summary>
        /// <para>Returns a Dictionary representing the keypair data.</para>
        /// <para>Throws a APIFailReturned exception on failure.</para>
        /// </summary>
        /// <param name="completeUrl">The complete URL, including tokens/ids that may be required.</param>
        /// <param name="seperator">Optional: The seperator for the key-pair information. Default is ':', recommended for GameJoltAPI.</param>
        /// <param name="checkForSuccess">Optional: If enabled, checks for a 'success' key, throws exception if not found. Highly recommended to remain enabled for GameJoltAPI.</param>
        /// <returns>A dictionary representing the key pair the API returned. String for both.</returns>
        public static Dictionary<string, string> getData(string completeUrl, char seperator = ':', bool checkForSuccess = true)
        {
            Uri u = new Uri(completeUrl);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(u);
            IAsyncResult res = req.BeginGetRequestStream(null, null);
            //while (!res.IsCompleted) ;
            Stream st = req.EndGetRequestStream(res);

            Dictionary<string, string> temp = new Dictionary<string, string>();
            
            using (StreamReader sr = new StreamReader(st))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();

                    if (line.Contains(seperator)) // otherwise do nothing, because it's not a key-pair
                    {
                        string[] split = line.Split(seperator); // Slightly more optimal than doing inline, as the runtime allocates memory for each split operation.
                        split[1] = split[1].Trim('\"'); //Trims the quotes around the value
                        temp.Add(split[0], split[1]);
                    }
                }
            }

            if (temp.ContainsKey("success") && checkForSuccess)
            {
                if (temp["success"] != "true")
                {
                    throw new APIFailReturned(temp["message"].ToString());
                }
                else
                {
                    return temp;
                }
            }
            else if (!checkForSuccess)
            {
                return temp;
            } 
            else
            {
                throw new APIFailReturned("Information received does not contain a success key.");
            }
        }
    }
}
