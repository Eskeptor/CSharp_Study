using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessorEx
{
    class Program
    {
        public static void Run1()
        {
            Console.Write("문장 입력: ");
            string text = Console.ReadLine();
            if (!string.IsNullOrEmpty(text))
            {
                Console.WriteLine("입력된 글자: " + text);
            }
#if OUTPUT_LOG
            else
            {
                Console.WriteLine("입력되지 않음");
            }
#endif
            Console.WriteLine();
        }
        public static void Run2()
        {
#if __x86__
            Console.WriteLine("x86 정의됨");
#endif
#if __x64__
            Console.WriteLine("x64 정의됨");
#endif
            Console.WriteLine();
        }
    }
}

namespace AttributeEx
{
    class Person : System.Attribute
    {
        private string mName;
        private int mAge;
        public Person(string _name, int _age)
        {
            mName = _name;
            mAge = _age;
        }
        public string Name
        {
            get { return mName; }
        }
        public int Age
        {
            get { return mAge; }
        }
    }

    [Person("홍길동", 17)]
    class Program
    {
        public static void Run()
        {

        }
    }
}

namespace ParamsEx
{
    enum Type
    {
        None, Line
    }

    class Program
    {
        public static int AddInt(int[] _arr)
        {
            int total = 0;
            foreach(int i in _arr)
            {
                total += i;
            }
            return total;
        }
        public static void AllPrint(Type _type, params object[] _arr)
        {
            switch (_type)
            {
                case Type.None:
                    {
                        foreach (object o in _arr)
                        {
                            Console.Write(o.ToString() + " ");
                        }
                        Console.WriteLine();
                        break;
                    }
                case Type.Line:
                    {
                        foreach (object o in _arr)
                        {
                            Console.WriteLine(o);
                        }
                        break;
                    }
            }
        }
        public static void Run()
        {
            int[] intArr = new int[] { 7, 4, 3, 2, 5, 6 };
            Console.WriteLine("Add: " + AddInt(intArr));
            Console.WriteLine();

            AllPrint(Type.Line, "ABCD", 1134, 45.221);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

namespace Win32API_Extern
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern int MessageBeep(uint uType);

        private static int TestMethod(uint _type)
        {
            return 0;
        }

        public static void Run()
        {
            Console.WriteLine("MessageBeep!");
            MessageBeep(0);
            Console.WriteLine();
        }
    }
}

namespace PointerEx
{
    class UnsafeEx
    {
        unsafe private static void Adder(int* _res, int _num1, int _num2)
        {
            *_res = _num1 + _num2;
        }

        public static void Run()
        {
            int result;
            unsafe
            {
                Adder(&result, 54, 66);
            }
            Console.WriteLine("54 + 66 = " + result);
            Console.WriteLine();
        }
    }

    class FixedEx
    {
        class Managed
        {
            public int mCount;
            public string mName;
        }

        public unsafe static void Run()
        {
            Managed inst = new Managed()
            {
                mCount = 5,
                mName = "NoNo"
            };

            fixed (int* pValue = &inst.mCount)
            {
                *pValue = 7;
            }
            Console.Write(inst.mCount + " ");
            fixed (char* pChar = inst.mName.ToCharArray())
            {
                int length = inst.mName.Length;
                for(int i = 0; i < length; i++)
                {
                    Console.Write(*(pChar + i));
                }
            }
            Console.WriteLine();
        }
    }
}

namespace RunEx
{
    class Program
    {
        static void Main(string[] args)
        {
            PreProcessorEx.Program.Run1();
            PreProcessorEx.Program.Run2();
            ParamsEx.Program.Run();
            Win32API_Extern.Program.Run();
            PointerEx.UnsafeEx.Run();
            PointerEx.FixedEx.Run();
        }
    }
}