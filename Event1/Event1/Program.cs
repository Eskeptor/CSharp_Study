using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event1
{
    class CallBackArg { }
    class PrimeCallBackArg : CallBackArg
    {
        private int mPrime;
        public PrimeCallBackArg(int _prime)
        {
            mPrime = _prime;
        }
        public int Prime
        {
            get { return mPrime; }
            set { mPrime = value; }
        }
    }
    class PrimeGenerator
    {
        public delegate void PrimeDelegate(object _sender, CallBackArg _arg);
        private PrimeDelegate mPrimeDelegate;
        public void AddDelegate(PrimeDelegate _delegate)
        {
            mPrimeDelegate = Delegate.Combine(mPrimeDelegate, _delegate) as PrimeDelegate;
        }
        public void RemoveDelegate(PrimeDelegate _delegate)
        {
            mPrimeDelegate = Delegate.Remove(mPrimeDelegate, _delegate) as PrimeDelegate;
        }
        public void Run(int _limit)
        {
            for(int i = 2; i <= _limit; i++)
            {
                if(IsPrime(i) && mPrimeDelegate != null)
                {
                    mPrimeDelegate(this, new PrimeCallBackArg(i));
                }
            }
        }
        private bool IsPrime(int _candidate)
        {
            if((_candidate & 1) == 0)
            {
                return _candidate == 2;
            }
            
            for(int i = 3; (i * i) <= _candidate; i += 2)
            {
                if ((_candidate % i) == 0)
                {
                    return false;
                }
            }

            return _candidate != 1;
        }
    }
    class Program
    {
        static int mSum;
        static void PrintPrime(object _sender, CallBackArg _arg)
        {
            Console.Write((_arg as PrimeCallBackArg).Prime + ", ");
        }
        static void SumPrime(object _sender, CallBackArg _arg)
        {
            mSum += (_arg as PrimeCallBackArg).Prime;
        }
        public static void Run()
        {
            PrimeGenerator generator = new PrimeGenerator();
            PrimeGenerator.PrimeDelegate printDelegate = PrintPrime;
            generator.AddDelegate(printDelegate);
            PrimeGenerator.PrimeDelegate sumDelegate = SumPrime;
            generator.AddDelegate(sumDelegate);

            generator.Run(10);
            Console.WriteLine();
            Console.WriteLine(mSum);

            generator.RemoveDelegate(sumDelegate);
            generator.Run(15);
            Console.WriteLine();
        }
    }
}

namespace Event2
{
    class EventCallBack : EventArgs
    {
        private int mEven;
        public EventCallBack(int _even)
        {
            mEven = _even;
        }
        public int Even
        {
            get { return mEven; }
        }
    }

    class EvenGenerator
    {
        public event EventHandler mEvenHandler;
        public void Run(int _limit)
        {
            for (int i = 1; i <= _limit; i++) 
            {
                if (i % 2 == 0 && mEvenHandler != null)
                {
                    mEvenHandler(this, new EventCallBack(i));
                }
            }
        }
    }

    class Program
    {
        private static int mSum;

        static void PrintEven(object _sender, EventArgs _args)
        {
            Console.Write((_args as EventCallBack).Even + " ");
        }

        static void SumEven(object _sender, EventArgs _args)
        {
            mSum += (_args as EventCallBack).Even; 
        }

        public static void Run()
        {
            EvenGenerator evenGenerator = new EvenGenerator();
            evenGenerator.mEvenHandler += PrintEven;
            evenGenerator.mEvenHandler += SumEven;

            evenGenerator.Run(16);
        }
    }
}

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {
            Event1.Program.Run();
            Console.WriteLine();
            Event2.Program.Run();
        }
    }
}