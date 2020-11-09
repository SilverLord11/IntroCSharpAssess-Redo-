using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessRedo
{
    class Util
    {

        /// <summary>
        /// Method for Message in Console.
        /// </summary>
        /// <param name="_msg">Message Displaying</param>
        /// <param name="_clr">Clear Screen</param>
        public static void Prompt(string _msg, bool _clr = false)
        {
            if (_clr)
                Console.Clear();
            Console.WriteLine(_msg);
        }

        /// <summary>
        /// Get input from user for Prompt
        /// </summary>
        /// <param name="_msg">Message Displaying</param>
        /// <param name="_clr">Clear Screen</param>
        /// <returns></returns>
        public static string Ask(string _msg, bool _clr = false)
        {
            Prompt(_msg, _clr);
            return Console.ReadLine();
        }
    }
}
