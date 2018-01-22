using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCL_DateTime1
{
    class Program
    {
        private static void Sum()
        {
            int num = 0;
            for (int i = 0; i < 10000000; i++)
                num += i;
        }
        private static void DateTimeBasic()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine("DateTime.Now: " + now);
            DateTime day = new DateTime(now.Year, 5, 5);
            Console.WriteLine("DateTime(now.Year, 5, 5): " + day);
            Console.WriteLine();
        }
        private static void Timer()
        {
            DateTime start = DateTime.Now;
            Sum();
            DateTime end = DateTime.Now;
            long clock = end.Ticks - start.Ticks;
            Console.WriteLine("0 ~ 10000000 더하기 걸린 시간");
            Console.WriteLine("Total Ticks: " + clock);
            Console.WriteLine("Millisecond: " + (clock / 10000));
            Console.WriteLine("Second: " + (clock / 10000000));

            Console.WriteLine();
        }
        private static void UTC()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now + " kind: " + now.Kind);
            DateTime utcTime = DateTime.UtcNow;
            Console.WriteLine(utcTime + " kind: " + utcTime.Kind);
            DateTime worldCup2002 = new DateTime(2002, 5, 31);
            Console.WriteLine(worldCup2002 + " kind: " + worldCup2002.Kind);
            worldCup2002 = new DateTime(2002, 5, 31, 0, 0, 0, DateTimeKind.Local);
            Console.WriteLine(worldCup2002 + " kind: " + worldCup2002.Kind);
            Console.WriteLine();
        }
        private static void UsingTimeSpan()
        {
            DateTime endOfYear = new DateTime(DateTime.Now.Year, 12, 31);
            DateTime now = DateTime.Now;
            Console.WriteLine("오늘 날짜: " + now);

            TimeSpan gap = endOfYear - now;
            Console.WriteLine("올해 남은 날짜: " + gap);
            Console.WriteLine();
        }
        private static void UsingStopwatch()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            Sum();
            stopwatch.Stop();
            Console.WriteLine("UsingStopwatch: 0 ~ 10000000 더하기 걸린 시간");
            Console.WriteLine("Total Ticks: " + stopwatch.ElapsedTicks);
            Console.WriteLine("Millisecond: " + (stopwatch.ElapsedTicks / 10000));
            Console.WriteLine("Second: " + (stopwatch.ElapsedTicks / 10000000));
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            DateTimeBasic();
            Timer();
            UTC();
            UsingTimeSpan();
            UsingStopwatch();
        }
    }
}
