using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalDesignPatterns.Builder.FirstStep
{
    // The String Builder is an .NET Framework built-in Builder which is used to build up Strings. It is a good starting point to familiarize yourself with the Builder concept.
    // In this example we will use the String Builder to output some HTML code.

    class StringBuilderExample
    {
        static void Main(string[] args)
        {
            string paragraph = "hello";
            StringBuilder sb = new StringBuilder();
            sb.Append($"<p> {paragraph} </p>" + "\n");
            sb.Append("<ul>" + "\n");
            sb.Append("  <li> hello </li>" + "\n");
            sb.Append("  <li> world </li>" + "\n");
            sb.Append("</ul>" + "\n");

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }

    }
}
