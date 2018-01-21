using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum1
{
    enum Days1
    {
        Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
    }
    [Flags]
    enum Days2
    {
        Sunday = 1, Monday, Tuesday = 5, Wednesday = 7, Thursday = 16, Friday = 32, Saturday = 64
    }
    class Program
    {
        static void Main(string[] args)
        {
            Days1 today = Days1.Sunday;
            Console.WriteLine(today);
            Console.WriteLine((int)today);

            Days2 days = Days2.Monday | Days2.Tuesday | Days2.Wednesday;
            Console.WriteLine(days.HasFlag(Days2.Sunday));
            Console.WriteLine(days.HasFlag(Days2.Monday));
            Console.WriteLine(days);
        }
    }
}
