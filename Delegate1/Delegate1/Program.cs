using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate1
{
    class Mathematics
    {
        delegate double CalcDelegate(double _num1, double _num2);
        CalcDelegate[] mMethod;
        public Mathematics()
        {
            mMethod = new CalcDelegate[] { Add, Sub, Multi, Div };
        }
        private double Add(double _num1, double _num2)
        {
            return _num1 + _num2;
        }
        private double Sub(double _num1, double _num2)
        {
            return _num1 - _num2;
        }
        private double Multi(double _num1, double _num2)
        {
            return _num1 * _num2;
        }
        private double Div(double _num1, double _num2)
        {
            return _num1 / _num2;
        }
        public double Calculate(char _op, double _num1, double _num2)
        {
            switch (_op)
            {
                case '+':
                    return mMethod[0](_num1, _num2);
                case '-':
                    return mMethod[1](_num1, _num2);
                case '*':
                    return mMethod[2](_num1, _num2);
                case '/':
                    return mMethod[3](_num1, _num2);
                default:
                    Console.WriteLine("잘못된 OP코드");
                    return -1;
            }
        }
        delegate double CalcMainDelegate(char _op, double _num1, double _num2);
        public void Run()
        {
            Mathematics mathematics = new Mathematics();
            CalcMainDelegate calc = mathematics.Calculate;

            double num1 = 4.2, num2 = 5.7;
            char op = '+';
            Console.WriteLine(num1 + " " + op + " " + num2 + " = " + calc(op, num1, num2));

            op = '*';
            Console.WriteLine(num1 + " " + op + " " + num2 + " = " + calc(op, num1, num2));
        }
    }

    class Mathematics2
    {
        delegate void CalcDelegate(double _num1, double _num2);
        private void Add(double _num1, double _num2)
        {
            Console.WriteLine(_num1 + " + " + _num2 + " = " + (_num1 + _num2));
        }
        private void Sub(double _num1, double _num2)
        {
            Console.WriteLine(_num1 + " - " + _num2 + " = " + (_num1 - _num2));
        }
        private void Multi(double _num1, double _num2)
        {
            Console.WriteLine(_num1 + " * " + _num2 + " = " + (_num1 * _num2));
        }
        private void Div(double _num1, double _num2)
        {
            Console.WriteLine(_num1 + " / " + _num2 + " = " + (_num1 / _num2));
        }
        public void Run()
        {
            CalcDelegate calc = Add;
            calc += Sub;
            calc += Multi;
            calc += Div;
            calc(7.9, 11.5);
        }
    }
}

namespace Delegate2
{
    delegate int GetResultDelegate();
    class Target
    {
        public void Do(GetResultDelegate _delegate)
        {
            Console.WriteLine(_delegate);
        }
    }
    class Source
    {
        public int GetResult()
        {
            return 10;
        }
        public void Test()
        {
            Target target = new Target();
            target.Do(new GetResultDelegate(this.GetResult));
        }
    }
}

namespace Delegate3
{
    class SortObject
    {
        private int[] mNumbers;
        public delegate bool CompareDelegate(int _arg1, int _arg2);
        public SortObject(int[] _numbers)
        {
            mNumbers = _numbers;
        }
        public void Sort(CompareDelegate _compare)
        {
            int temp;
            for(int i = 0; i < mNumbers.Length; i++)
            {
                int lowPos = i;
                for(int j = i + 1; j < mNumbers.Length; j++)
                {
                    if(_compare(mNumbers[j], mNumbers[lowPos]))
                    {
                        lowPos = j;
                    }
                }
                temp = mNumbers[lowPos];
                mNumbers[lowPos] = mNumbers[i];
                mNumbers[i] = temp;
            }
        }
        public void Display()
        {
            foreach(int num in mNumbers)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
        public static bool AscendingCompare(int _arg1, int _arg2)
        {
            return _arg1 < _arg2;
        }
        public static bool DescendingCompare(int _arg1, int _arg2)
        {
            return _arg1 > _arg2;
        }
        public static void Run()
        {
            int[] intArray = new int[] { 9, 11, 7, 3, 1, 2 };
            SortObject sort = new SortObject(intArray);
            sort.Sort(AscendingCompare);
            sort.Display();
            Console.WriteLine();
            sort.Sort(DescendingCompare);
            sort.Display();
        }
    }
}

namespace Delegate4
{
    class Person
    {
        private int mAge;
        private string mName;
        public Person(int _age, string _name)
        {
            mAge = _age;
            mName = _name;
        }
        public int Age
        {
            get { return mAge; }
            set { mAge = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public override string ToString()
        {
            return mName + " : " + mAge;
        }
    }

    class SortPerson
    {
        public delegate bool CompareDelegate(Person _p1, Person _p2); 
        private Person[] mPersons;
        public SortPerson(Person[] _persons)
        {
            mPersons = _persons;
        }
        public static bool AscendingSort(Person _p1, Person _p2)
        {
            return _p1.Age < _p2.Age;
        }
        public static bool DescendingSort(Person _p1, Person _p2)
        {
            return _p1.Age > _p2.Age;
        }
        public void Display()
        {
            foreach(Person person in mPersons)
            {
                Console.WriteLine(person);
            }
        }
        public void Sort()
        {
            SortEngine(0, mPersons.Length - 1);
        }
        private void SortEngine(int _left, int _right)
        {
            int left = _left;
            int right = _right;
            int pivot = (left + right) / 2;
            Person temp;
            while (left < right)
            {
                if (mPersons[pivot].Age > mPersons[right].Age)   // mPersons[pivot].Age > mPersons[right].Age
                {
                    if (mPersons[pivot].Age < mPersons[left].Age)   // mPersons[pivot].Age < mPersons[left].Age
                    {
                        
                        temp = mPersons[left];
                        mPersons[left] = mPersons[right];
                        mPersons[right] = temp;
                        left++;
                        right--;
                    }
                    else
                    {
                        left++;
                    }
                }
                else
                {
                    right--;
                }
            }
            if(_left < _right)
            {
                if (_left != pivot - 1)
                {
                    SortEngine(_left, pivot - 1);
                }
                if (_right != pivot + 1)
                {
                    SortEngine(pivot, _right);
                }
            }
        }
        public static void Run(CompareDelegate _delegate)
        {
            Person[] persons = new Person[] { new Person(13, "홍길동"), new Person(55, "김김김"), new Person(31, "노네임"),
                new Person(22, "트수"), new Person(66, "킹도"), new Person(54, "택기")};
            SortPerson sortPerson = new SortPerson(persons);
            sortPerson.Sort();
            sortPerson.Display();
        }
    }
}

namespace Run
{
    class Program
    {
        static void Main(string[] args)
        {
            new Delegate1.Mathematics().Run();
            Console.WriteLine();
            new Delegate1.Mathematics2().Run();
            Console.WriteLine();
            new Delegate2.Source().Test();
            Console.WriteLine();
            Delegate3.SortObject.Run();
            Console.WriteLine();
            Delegate4.SortPerson.Run(Delegate4.SortPerson.AscendingSort);
        }
    }
}