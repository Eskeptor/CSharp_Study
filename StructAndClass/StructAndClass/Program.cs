using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueAndReference
{
    class Program
    {
        public static void Run()
        {
            StructAndClass.Vector v1;
            v1.X = 5;
            v1.Y = 10;
            StructAndClass.Vector v2 = v1;  // Value
            v2.X = 6;
            v2.Y = 12;
            Console.WriteLine("v1: X = " + v1.X + ", Y = " + v1.Y);
            Console.WriteLine("v2: X = " + v2.X + ", Y = " + v2.Y);

            StructAndClass.Point p1 = new StructAndClass.Point();
            p1.X = 11;
            p1.Y = 22;
            StructAndClass.Point p2 = p1;   // Reference
            p2.X = 15;
            p2.Y = 30;
            Console.WriteLine("p1: X = " + p1.X + ", Y = " + p1.Y);
            Console.WriteLine("p2: X = " + p2.X + ", Y = " + p2.Y);
            Console.WriteLine();

            p1 = null;
            UsingRef.ShallowCopy(p1);
            Console.WriteLine("p1: X = " + p1);
            UsingRef.CopyUsingRef(ref p1);
            Console.WriteLine("p1: X = " + p1.X + ", Y = " + p1.Y);
            Console.WriteLine();

            double num1 = 5.2, num2 = 2.3, result;
            if(UsingOut.Divide(num1, num2, out result))
            {
                Console.WriteLine(num1 + " / " + num2 + " = " + result);
            }
        }
    }

    class UsingRef
    {
        public static void ShallowCopy(StructAndClass.Point _point)
        {
            _point = new StructAndClass.Point()
            {
                X = 6,
                Y = 12
            };
        }
        public static void CopyUsingRef(ref StructAndClass.Point _point)
        {
            _point = new StructAndClass.Point()
            {
                X = 7,
                Y = 14
            };
        }
    }

    class UsingOut
    {
        public static bool Divide(double _num1, double _num2, out double result)
        {
            if(_num2 == 0)
            {
                result = 0;
                return false;
            }
            result = _num1 / _num2;
            return true;
        }
    }
}

namespace StructAndClass
{
    struct Vector
    {
        public int X;
        public int Y;
    }
    class Point
    {
        public int X;
        public int Y;
    }
    class Program
    {
        static void Main(string[] args)
        {
            ValueAndReference.Program.Run();
        }
    }
}
