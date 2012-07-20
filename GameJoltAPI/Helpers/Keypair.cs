using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

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
        public static Dictionary<string, string> getData(string completeUrl)
        {
            Uri u = new Uri(completeUrl);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(u);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream st = res.GetResponseStream();

            Dictionary<string, string> temp = new Dictionary<string, string>();
            
            /* Read the stream line by line, to parse the gamejoltapi format
             * TODO: add success/failure checks, parsing checks
             */
            using (StreamReader sr = new StreamReader(st))
            {
                while (sr.Peek() >= 0)
                {
                    try
                    {
                        string line = sr.ReadLine();
                        temp.Add(line.Split(':')[0], line.Split(':')[1]);
                    }
                    catch (Exception e)
                    {
                        // do something
                    }
                }
            }

            return temp;
        }
    }
}
