using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceCallBack
{
    interface ISource
    {
        int GetResult();
    }
    class Source : ISource
    {
        public int GetResult()
        {
            return 10;
        }
        public void Run()
        {
            Target target = new Target();
            target.Do(this);
        }
    }
    class Target
    {
        public void Do(ISource obj)
        {
            Console.WriteLine(obj.GetResult());
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            new Source().Run();
            UsingIComparer.IntegerCompare.Run();
            UsingIEnumerable.NoteBook.Run();
        }
    }
}

namespace UsingIComparer
{
    class IntegerCompare : IComparer
    {
        public int Compare(object x, object y)
        {
            int xValue = (int)x;
            int yValue = (int)y;

            if (xValue > yValue)
                return -1;
            else if (xValue == yValue)
                return 0;
            else
                return 1;
        }
        public static void Run()
        {
            int[] arr = new int[] { 5, 9, 1, 3, 2 };
            Array.Sort(arr, new IntegerCompare());

            IEnumerator enumerator = arr.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current + " ");
            }
            Console.WriteLine();
        }
    }
}

namespace UsingIEnumerable
{
    class HardWare
    {
        private string mUID;
        private string mName;
        public HardWare(string _uid, string _name)
        {
            mUID = _uid;
            mName = _name;
        }
        public string UID
        {
            get { return mUID; }
        }
        public string Name
        {
            get { return mName; }
        }
        public virtual void Info()
        {
            Console.WriteLine("기기명: " + Name);
            Console.WriteLine("기기ID: " + UID);
        }
    }
    class USB : HardWare
    {
        private int mStorage;
        private char mDisk;
        public USB(string _uid, string _name, int _storage, char _disk) : base(_uid, _name)
        {
            mStorage = _storage;
            mDisk = _disk;
        }
        public override string ToString()
        {
            return "UID : " + UID;
        }
        public override void Info()
        {
            base.Info();
            Console.WriteLine("용량: " + mStorage);
            Console.WriteLine("할당디스크: " + mDisk);
        }
    }
    class NoteBook : HardWare, IEnumerable
    {
        private USB[] mUSBs = null;
        private bool mIsBoot;
        public NoteBook(string _uid, string _name) : base(_uid, _name)
        {
            mIsBoot = false;
        }
        public static void Run()
        {
            NoteBook noteBook = new NoteBook("NTB33201", "노트북1");
            noteBook.Shutdown();
            noteBook.Boot();
            noteBook.DisConnectUSB();
            noteBook.ConnectUSB();
            noteBook.USBList();
            noteBook.Shutdown();
        }
        private void Boot()
        {
            Info();
            Console.WriteLine("System: 부팅중...");
            mIsBoot = true;
            Console.WriteLine("System: 부팅완료");
        }
        private void Shutdown()
        {
            if(mUSBs != null)
            {
                DisConnectUSB();
            }
            if (mIsBoot)
            {
                Console.WriteLine("System: 시스템 종료중...");
                mIsBoot = false;
            }
            else
            {
                Console.WriteLine("System - Error: 시스템이 켜지지 않음");
            }
        }
        private void USBList()
        {
            IEnumerator enumerator = this.GetEnumerator();

            Console.WriteLine("System: USB 목록");
            if (mUSBs == null)
            {
                Console.WriteLine("System - Error: USB가 연결되지 않음");
            }
            else
            {
                foreach (USB usb in mUSBs)
                {
                    usb.Info();
                }
            }
        }
        private void ConnectUSB()
        {
            if (mUSBs != null)
            {
                Console.WriteLine("System - Error: 이미 연결되어 있음");
            }
            else
            {
                Console.WriteLine("System: USB 연결중...");
                mUSBs = new USB[] { new USB("BB122AD", "USB1", 32, 'D'), new USB("ME543DC", "USB2", 64, 'E') };
                Console.WriteLine("System: USB 연결완료");
            }
        }
        private void DisConnectUSB()
        {
            if (mUSBs != null)
            {
                Console.WriteLine("System: USB 연결해제중...");
                mUSBs = null;
                Console.WriteLine("System: USB 연결해제완료");
            }
            else
            {
                Console.WriteLine("System - Error: USB가 연결되지 않음");
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new USBEnumerator(mUSBs);
        }
    }
    class USBEnumerator : IEnumerator
    {
        private object[] mUSBs;
        private int mCur;
        private int mLength;

        public USBEnumerator(USB[] _usbs)
        {
            mUSBs = _usbs;
            mCur = -1;
            mLength = _usbs.Length;
        }

        public object Current
        {
            get { return mUSBs[mCur]; }
        }

        public bool MoveNext()
        {
            if (mCur >= mLength - 1)
            {
                return false;
            }
            else
            {
                mCur++;
                return true;
            }
        }

        public void Reset()
        {
            mCur = -1;
        }
    }
}