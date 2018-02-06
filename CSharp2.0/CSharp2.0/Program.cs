using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp2._0
{
    class NullOp
    {
        public static void Run()
        {
            string txt = null;
            Console.WriteLine(txt ?? "(null)");
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GenericTest.Stack<int>.Run();
            GenericTest.Utility.Run();
            NullOp.Run();
            GenericTest.YieldReturn.Run();
            GenericTest.Person.Run();
        }
    }
}

namespace GenericTest
{
    public class Utility
    {
        public static void WriteLog<T>(T _data)
        {
            string str = string.Format("{0} : {1}", DateTime.Now, _data);
            Console.WriteLine(str);
        }
        public static T Max<T>(T _data1, T _data2) where T : IComparable
        {
            if(_data1.CompareTo(_data2) >= 0)
            {
                return _data1;
            }
            return _data2;
        }
        public static void Run()
        {
            WriteLog(3.14);
            WriteLog("Hi");
            WriteLog(123456);
            Console.WriteLine();
            Console.WriteLine("Max(73, 55): " + Max(73, 55));
            Console.WriteLine("Max(abc, efg): " + Max("abc", "efg"));
            Console.WriteLine();
        }
    }
    public class YieldReturn
    {
        public static IEnumerable<int> Next(int _max)
        {
            int start = 1;
            while (true)
            {
                if(_max < start)
                {
                    yield break;
                }
                yield return start;
                start++;
            }
        }
        public static void Run()
        {
            foreach (int i in YieldReturn.Next(20))
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    public class Person
    {
        private string name;
        private int age;
        private Nullable<bool> married;
        public Person(string _name, int _age, bool _married)
        {
            name = _name;
            age = _age;
            married = _married;
        }
        public Person(string _name, int _age)
        {
            name = _name;
            age = _age;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("이름: " + name + Environment.NewLine);
            str.Append("나이: " + age + Environment.NewLine);
            str.Append("결혼: " + married + Environment.NewLine);
            return str.ToString();
        }
        public static void Run()
        {
            Person person1 = new Person("홍길동", 44);
            Person person2 = new Person("김길동", 33, true);
            Console.WriteLine(person1);
            Console.WriteLine(person2);
            Console.WriteLine();
        }
    }

    public class Stack<T>
    {
        private T[] datas;
        private int curPoint;
        public Stack(int _size)
        {
            datas = new T[_size];
            curPoint = -1;
        }
        public void Push(T _data)
        {
            if (curPoint < datas.Length - 1)
            {
                datas[++curPoint] = _data;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("데이터가 꽉찼습니다.");
                Console.WriteLine();
            }
        }
        public T Pop()
        {
            if (curPoint != -1)
                return datas[curPoint--];
            else
            {
                Console.WriteLine();
                Console.WriteLine("데이터가 없습니다.");
                return default(T);
            }
        }
        public T Top()
        {
            if (curPoint != 0)
                return datas[curPoint];
            else
            {
                Console.WriteLine();
                Console.WriteLine("데이터가 없습니다.");
                return default(T);
            }
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for(int i = 0; i <= curPoint; i++)
            {
                str.Append(datas[i]);
                if (i != curPoint)
                    str.Append(", ");
            }
            return str.ToString();
        }
        public static void Run()
        {
            Stack<int> stack = new Stack<int>(5);
            for(int i = 0; i < 5; i++)
            {
                stack.Push(i + i * 2);
            }
            Console.WriteLine("stack: " + stack);
            Console.WriteLine("Pop: " + stack.Pop());
            Console.WriteLine("stack: " + stack);
            Console.WriteLine("Top: " + stack.Top());
            Console.WriteLine();
        }
    }
}