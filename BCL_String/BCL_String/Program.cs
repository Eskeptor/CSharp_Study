using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCL_String
{
    class Program
    {
        static void PrintArray(string[] _arr)
        {
            int length = _arr.Length;
            for (int i = 0; i < length; i++)
            {
                Console.Write(_arr[i]);
                if (i != length - 1)
                    Console.Write(", ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            string str = "Hello World!!";

            Console.WriteLine(str + " Contains(\"Hello\"): " + str.Contains("Hello"));
            Console.WriteLine(str + " Contains(\"Helo\"): " + str.Contains("Helo"));
            Console.WriteLine(str + " EndsWith(\"World!!\"): " + str.EndsWith("World!!"));
            Console.WriteLine(str + " EndsWith(\"World!\"): " + str.EndsWith("World!"));
            Console.WriteLine(str + " GetHashCode(): " + str.GetHashCode());
            Console.WriteLine("Hi GetHashCode(): " + "Hi".GetHashCode());
            Console.WriteLine(str + " IndexOf(\"World!!\"): " + str.IndexOf("World!!"));
            Console.WriteLine(str + " IndexOf(\"Hi!\"): " + str.IndexOf("Hi!"));
            Console.WriteLine(str + " Replace(\"!!\", \"?\"): " + str.Replace("!!", "?"));
            Console.WriteLine(str + " Replace(\"nono\", \"right\"): " + str.Replace("nono", "right"));
            Console.Write(str + " Split('l'): ");
            PrintArray(str.Split('l'));
            Console.Write(str + " Split(' '): ");
            PrintArray(str.Split(' '));
            Console.WriteLine(str + " StartsWith(\"Hello\"): " + str.StartsWith("Hello"));
            Console.WriteLine(str + " StartsWith(\"World!!\"): " + str.StartsWith("World!!"));
            Console.WriteLine(str + " Substring(1): " + str.Substring(1));
            Console.WriteLine(str + " Substring(2, 3): " + str.Substring(2, 3));
            Console.WriteLine(str + " ToLower(): " + str.ToLower());
            Console.WriteLine(str + " ToUpper(): " + str.ToUpper());
            Console.WriteLine("\" He llo   Wor ld ! !  \" Trim(): " + " He llo   Wor ld ! !  ".Trim());
            Console.WriteLine(str + " Trim('H'): " + str.Trim('H'));
            Console.WriteLine(str + " Trim('W'): " + str.Trim('W'));
            Console.WriteLine(str + " Trim('!'): " + str.Trim('!'));
            Console.WriteLine(str + " Length: " + str.Length);
            Console.WriteLine("Hello Length: " + "Hello".Length);
            Console.WriteLine("Hello != World: " + ("Hello" != "World"));
            Console.WriteLine("Hello == World: " + ("Hello" == "World"));
            Console.WriteLine("Hello == HELLO: " + ("Hello" == "HELLO"));
            Console.WriteLine("Hello.Equals(\"HELLO\", StringComparison.OrdinalIgnoreCase): " + "Hello".Equals("HELLO", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine();
            string str2 = "Hello {0} {1}";
            string output = string.Format(str2, "월드", "!!!");
            Console.WriteLine("str2: " + str2);
            Console.WriteLine("output: " + output);

            string str3 = "{0, -10} * {1} == {2, 10}";
            Console.WriteLine(str3, 5, 6, 5 * 6);
        }
    }
}
