using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCL_Thread
{
    class ThreadJoinEx
    {
        private static void ThreadFunc(object _second)
        {
            Console.WriteLine("{0}초 후에 스레드 종료", (int)_second);
            Thread.Sleep(1000 * (int)_second);
            Console.WriteLine("스레드 종료");
        }
        public static void Run()
        {
            Console.WriteLine("ThreadJoinEx.Run Start");
            Thread thread = new Thread(ThreadFunc);
            thread.IsBackground = true;
            thread.Start(5);
            thread.Join();
            Console.WriteLine("ThreadJoinEx.Run End");
            Console.WriteLine();
        }
    }
    class ObjectParamEx
    {
        class Point
        {
            private int xpos, ypos;
            public Point(int _xpos, int _ypos)
            {
                xpos = _xpos;
                ypos = _ypos;
            }
            public int Xpos { get { return xpos; } }
            public int Ypos { get { return ypos; } }
        }
        private static void ThreadFunc(object _initValue)
        {
            Point point = _initValue as Point;
            Console.WriteLine("({0}, {1})", point.Xpos, point.Ypos);
        }
        public static void Run()
        {
            Console.WriteLine("ObjectParamEx.Run Start");
            Thread thread = new Thread(ThreadFunc);
            thread.Start(new Point(4, 6));
            thread.Join();
            Console.WriteLine("ObjectParamEx.Run End");
            Console.WriteLine();
        }
    }
    class ThreadPrimeCalcEx
    {
        private static void CountPrimeNumber(object _initValue)
        {
            int primeCandidate = (int)_initValue;
            int total = 0;
            for (int i = 2; i < primeCandidate; i++) 
            {
                if (IsPrime(i))
                    total++;
            }
            Console.WriteLine("숫자 {0}까지의 소수 개수: {1}", primeCandidate, total);
        }
        private static bool IsPrime(int _candidate)
        {
            if ((_candidate & 1) == 0)
                return _candidate == 2;

            for (int i = 3; (i * i) <= _candidate; i += 2)
            {
                if ((_candidate % i) == 0)
                    return false;
            }
            return _candidate != 1;
        }
        public static void Run()
        {
            Console.WriteLine("ThreadPrimeCalcEx.Run Start");
            while(true)
            {
                Console.WriteLine("(종료: x)");
                Console.Write("숫자 입력: ");
                string input = Console.ReadLine();
                if (input.Equals("x", StringComparison.OrdinalIgnoreCase))
                    break;
                Thread thread = new Thread(CountPrimeNumber);
                thread.Start(Int32.Parse(input));
                thread.Join();
            }
            Console.WriteLine("ThreadPrimeCalcEx.Run End");
            Console.WriteLine();
        }
    }
    class ThreadLockEx
    {
        class Point
        {
            private int xpos, ypos;
            public Point()
            {
                xpos = 0;
                ypos = 0;
            }
            public int Xpos
            {
                get { return xpos; }
                set { xpos = value; }
            }
            public int Ypos
            {
                get { return ypos; }
                set { ypos = value; }
            }
            public override string ToString()
            {
                return string.Format("({0}, {1})", xpos, ypos);
            }
        }

        private static void ThreadFunc(object _initValue)
        {
            Point point = _initValue as Point;
            for(int i = 0; i < 10000; i++)
            {
                lock (point)
                {
                    point.Xpos += 1;
                    point.Ypos += 2;
                }
            }
        }
        public static void Run()
        {
            Console.WriteLine("ThreadLockEx.Run Start");
            Point point = new Point();
            Thread thread1 = new Thread(ThreadFunc);
            Thread thread2 = new Thread(ThreadFunc);
            thread1.Start(point);
            thread2.Start(point);
            thread1.Join();
            thread2.Join();
            Console.WriteLine("Point: " + point);
            Console.WriteLine("ThreadLockEx.Run End");
            Console.WriteLine();
        }
    }
    class EventWaitHandleEx
    {
        class Custom
        {
            public int second = 0;
            public EventWaitHandle eventWait = null;
        }
        private static void ThreadFunc(object _initValue)
        {
            Custom custom = _initValue as Custom;
            Console.WriteLine("{0}초 후에 스레드 종료", custom.second);
            Thread.Sleep(1000 * custom.second);
            Console.WriteLine("스레드 종료");
            custom.eventWait.Set();
        }
        public static void Run()
        {
            Console.WriteLine("EventWaitHandleEx.Run Start");
            Custom custom = new Custom()
            {
                second = 3,
                eventWait = new EventWaitHandle(false, EventResetMode.ManualReset)
            };
            Thread thread = new Thread(ThreadFunc);
            thread.IsBackground = true;
            thread.Start(custom);
            custom.eventWait.WaitOne();
            Console.WriteLine("EventWaitHandleEx.Run End");
            Console.WriteLine();
        }
    }

    class DelegateAsyncEx
    {
        class Calc
        {
            public static int StartEndAdder(int _start, int _end)
            {
                int total = 0;
                for (int i = _start; i <= _end; i++)
                {
                    total += i;
                }
                return total;
            }
        }
        public delegate int CalcMethod(int _start, int _end);
        private static void CalcCompleted(IAsyncResult _async)
        {
            CalcMethod calc = _async.AsyncState as CalcMethod;
            int result = calc.EndInvoke(_async);
            Console.WriteLine("1 ~ 100 더한값 : " + result);
        }
        public static void RunWithInvoke()
        {
            Console.WriteLine("DelegateAsyncEx.RunWithInvoke Start");
            CalcMethod calc = new CalcMethod(Calc.StartEndAdder);
            IAsyncResult async = calc.BeginInvoke(1, 100, null, null);
            async.AsyncWaitHandle.WaitOne();
            int result = calc.EndInvoke(async);
            Console.WriteLine("1 ~ 100 더한값 : " + result);
            Console.WriteLine("DelegateAsyncEx.RunWithInvoke End");
            Console.WriteLine();
        }
        public static void RunWithCallBack()
        {
            Console.WriteLine("DelegateAsyncEx.RunWithCallBack Start");
            CalcMethod calc = new CalcMethod(Calc.StartEndAdder);
            calc.BeginInvoke(1, 100, CalcCompleted, calc);
            Console.WriteLine("DelegateAsyncEx.RunWithCallBack End");
            Console.WriteLine();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ThreadJoinEx.Run();
            ObjectParamEx.Run();
            ThreadPrimeCalcEx.Run();
            ThreadLockEx.Run();
            EventWaitHandleEx.Run();
            DelegateAsyncEx.RunWithInvoke();
            DelegateAsyncEx.RunWithCallBack();
        }
    }
}
