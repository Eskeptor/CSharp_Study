using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer1
{
    struct Constants
    {
        public const string VENDOR = "제조사";
        public const string MODEL = "모델명";
    }
    class GraphicCard
    {
        private string mVendor;
        private string mModel;
        public GraphicCard(string _vendor, string _model)
        {
            mVendor = _vendor;
            mModel = _model;
        }
        public string this[string _index]
        {
            get
            {
                switch (_index)
                {
                    case Constants.VENDOR:
                        return mVendor;
                    case Constants.MODEL:
                        return mModel;
                    default:
                        return null;
                }
            }
        }
        public override string ToString()
        {
            return Constants.VENDOR + ": " + mVendor + Environment.NewLine + Constants.MODEL + ": " + mModel;
        }
    }
    class IntegerText
    {
        private char[] mStr;
        private int mLength;
        public IntegerText(int _num)
        {
            mStr = _num.ToString().ToCharArray();
            mLength = mStr.Length;
        }
        public int Length
        {
            get { return mLength; }
        }
        public char this[int _index]
        {
            get { return mStr[_index]; }
            set { mStr[_index] = value; }
        }
        public override string ToString()
        {
            return new string(mStr);
        }
        public int ToInt32()
        {
            return Int32.Parse(ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IntegerText integerText = new IntegerText(175664);
            Console.WriteLine(integerText);
            Console.WriteLine(integerText[2]);
            Console.WriteLine(integerText.ToInt32());
            Console.WriteLine();

            GraphicCard card = new GraphicCard("ZOTAC", "Nvidia Geforce GTX 1050");
            Console.WriteLine(card["제조사"]);
            Console.WriteLine(card["모델명"]);
            Console.WriteLine(card);
        }
    }
}
