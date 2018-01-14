using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayTest
{
    class ArrayTest
    {
        public void OutputArrayInfo(Array _array)
        {
            Console.WriteLine("배열의 차원 수: " + _array.Rank);
            Console.WriteLine("배열의 요소 수: " + _array.Length);
            Console.WriteLine();
        }

        public void OutputArrayElements(string _title, Array _array)
        {
            Console.WriteLine("[" + _title + "]");
            foreach (Object ob in _array)
            {
                Console.Write(ob + ", ");
            }
            Console.WriteLine();
        }

        public void Run()
        {
            bool[,] boolArray = new bool[,] { { true, false }, { false, false } };
            OutputArrayInfo(boolArray);

            int[] intArray = new int[] { 1, 2, 3, 4, 0, 5 };
            OutputArrayInfo(intArray);

            OutputArrayElements("원본 intArray", intArray);
            Array.Sort(intArray);
            OutputArrayElements("Array.Sort 후 intArray", intArray);

            int[] copyArray = new int[intArray.Length];
            Array.Copy(intArray, copyArray, intArray.Length);
            OutputArrayElements("복사한 copyArray", copyArray);

            Console.WriteLine();
        }
    }
}

namespace OverrideTest
{
    class Mammal
    {
        virtual public void Move()
        {
            Console.WriteLine("이동한다.");
        }
    }
    class Lion : Mammal
    {
        public override void Move()
        {
            Console.WriteLine("네 발로 움직인다.");
        }
    }
    class Whale : Mammal
    {
        public override void Move()
        {
            Console.WriteLine("수영한다.");
        }
    }
    class Human : Mammal
    {
        public override void Move()
        {
            Console.WriteLine("두발로 걷는다.");
        }
    }
    class Program
    {
        public void Run()
        {
            Lion lion = new Lion();
            Mammal mammal = lion;
            mammal.Move();

            Human human = new Human();
            mammal = human;
            mammal.Move();

            Console.WriteLine();
        }
    }
}

namespace OverrideEqual
{
    class Book
    {
        private decimal mIsbn;
        private string mTitle;
        private string mContents;
        public Book(decimal _isbn, string _title, string _contents)
        {
            mIsbn = _isbn;
            mTitle = _title;
            mContents = _contents;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Book book = obj as Book;
            if (book == null)
            {
                return false;
            }
            return mIsbn == book.mIsbn;
        }
        public override int GetHashCode()
        {
            return mIsbn.GetHashCode();
        }
    }

    class Program
    {
        public void Run()
        {
            Book book1 = new Book(9788998139018, "책1", "빠바ㅃ밥");
            Book book2 = new Book(9788998139018, "책1", "빠바ㅃ밥");
            Book book3 = new Book(9788998139019, "책2", "빠바ㅃ밥");

            Console.WriteLine("book1 == book2 : " + book1.Equals(book2));
            Console.WriteLine("book1 == book3 : " + book1.Equals(book3));
            Console.WriteLine();
        }
    }
}

namespace OverloadOperator
{
    class Kilogram
    {
        private double mass;
        public Kilogram(double _value)
        {
            mass = _value;
        }
        public static Kilogram operator +(Kilogram _op1, Kilogram _op2)
        {
            return new Kilogram(_op1.mass + _op2.mass);
        }
        public override string ToString()
        {
            return mass.ToString();
        }
        public double getMass()
        {
            return mass;
        }
    }
    class Program
    {
        public void Run()
        {
            Kilogram k1 = new Kilogram(3);
            Kilogram k2 = new Kilogram(4);
            Console.WriteLine(k1.getMass() + " + " + k2.getMass() + " = " + (k1 + k2));
            Console.WriteLine();
        }
    }
}

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            new ArrayTest.ArrayTest().Run();
            new OverrideTest.Program().Run();
            new OverrideEqual.Program().Run();
            new OverloadOperator.Program().Run();
        }
    }
}
