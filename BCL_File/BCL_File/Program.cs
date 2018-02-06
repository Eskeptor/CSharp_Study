using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCL_File
{
    class StreamWriterEx
    {
        public static void Run()
        {
            using (FileStream fs = new FileStream("test_streamwriter.log", FileMode.Create))
            {
                StreamWriter streamWriter = new StreamWriter(fs, Encoding.UTF8);
                streamWriter.WriteLine("Hello World");
                streamWriter.WriteLine("하잇");
                streamWriter.WriteLine(1234);
                streamWriter.Close();
                fs.Close();
            }

            using (FileStream fs = new FileStream("test_streamwriter.log", FileMode.Open))
            {
                StreamReader streamReader = new StreamReader(fs, Encoding.UTF8);
                Console.WriteLine(streamReader.ReadToEnd());
                Console.WriteLine();
                streamReader.Close();
                fs.Close();
            }
        }
    }
    class BinaryWriterEx
    {
        public static void Run()
        {
            using (FileStream fs = new FileStream("test_binarywriter.log", FileMode.Create))
            {
                BinaryWriter binaryWriter = new BinaryWriter(fs);
                binaryWriter.Write("Hello World" + Environment.NewLine);
                binaryWriter.Write("하잇" + Environment.NewLine);
                binaryWriter.Write(1234);
                binaryWriter.Close();
                fs.Close();
            }
            using (FileStream fs = new FileStream("test_binarywriter.log", FileMode.Open))
            {
                BinaryReader binaryReader = new BinaryReader(fs);
                string str;
                try
                {
                    while ((str = binaryReader.ReadString()) != null)
                    {
                        Console.Write(str);
                    }
                }
                catch (Exception e)
                {
                    binaryReader.Close();
                }
                Console.WriteLine();
                fs.Close();
            }
        }
    }
    class DirectoryEx
    {
        public static void Run()
        {
            string path = Directory.GetCurrentDirectory();
            foreach(string list in Directory.GetFiles(path))
            {
                Console.WriteLine(list);
            }
            Console.WriteLine();
        }
    }
    class PathEx
    {
        public static void Run()
        {
            string samplePath = @"c:\temp\bin\Debug\app.exe";
            Console.WriteLine("ChangeExtension => " + Path.ChangeExtension(samplePath, ".dll"));
            Console.WriteLine("GetDirectoryName => " + Path.GetDirectoryName(samplePath));
            Console.WriteLine("GetFullPath => " + Path.GetFullPath(samplePath));
            Console.WriteLine("GetFileName => " + Path.GetFileName(samplePath));
            Console.WriteLine("GetFileNameWithoutExtension => " + Path.GetFileNameWithoutExtension(samplePath));
            Console.WriteLine("GetExtension => " + Path.GetExtension(samplePath));
            Console.WriteLine("GetPathRoot => " + Path.GetPathRoot(samplePath));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriterEx.Run();
            BinaryWriterEx.Run();
            DirectoryEx.Run();
            PathEx.Run();
        }
    }
}
