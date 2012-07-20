using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameJoltAPI;
using GameJoltAPI.Exceptions;
using GameJoltAPI.Helpers;

namespace TestAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Check if it returns properly, i.e. throws an exception on failure. */
            try
            {
                Console.WriteLine(Dump.getData("http://gamejolt.com/api/game/v1/data-store/?format=dump"));
            }
            catch (DumpFormatFailReturned e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.Read();
        }
    }
}
