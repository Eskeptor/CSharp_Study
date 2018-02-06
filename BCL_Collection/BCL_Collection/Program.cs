using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCL_Collection
{
    class HashtableEx
    {
        private static void PrintTable(Hashtable _table)
        {
            foreach(object obj in _table.Keys)
            {
                Console.WriteLine("{0} => {1}", obj, _table[obj]);
            }
            Console.WriteLine();
        }
        public static void Run()
        {
            Hashtable hashtable = new Hashtable();

            hashtable.Add("key1", "C");
            hashtable.Add("key2", "C++");
            hashtable.Add("key3", "Java");
            hashtable.Add("key4", "C#");

            Console.WriteLine("key4: " + hashtable["key4"]);

            hashtable.Remove("key3");
            PrintTable(hashtable);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
