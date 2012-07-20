using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using GameJoltAPI.Exceptions;

namespace GameJoltAPI.Helpers
{
    /// <summary>
    /// <para>Provides a list of functions used to manipulate the Dump data format the API provides.</para>
    /// <para>See: http://gamejolt.com/api/doc/game/formats/dump/ for further information.</para>
    /// <para>@author Christian "HyperGod" Bosence</para>
    /// <para>@version 0.1.0.0</para>
    /// </summary>
    public static class Dump
    {
        /// <summary>
        /// <para>Returns an object representing the data. Stripped success line.</para>
        /// <para>Thows a DumpFormatFailReturned exception on failure.</para>
        /// </summary>
        /// <param name="completeUrl">The complete URL, including tokens/ids that may be required.</param>
        /// <returns></returns>
        public static Object getData(string completeUrl) {
            /* Webclient.DownloadString is known to suffer performance issues. Best we download the stream ourselves. */
            Uri u = new Uri(completeUrl);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(u);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream st = res.GetResponseStream();
            StreamReader sr = new StreamReader(st);
            
            if (sr.ReadLine().Contains("SUCCESS"))
            {
                return sr.ReadToEnd();
            }
            else
            {
                throw new DumpFormatFailReturned(sr.ReadLine());
            }
        }
    }
}