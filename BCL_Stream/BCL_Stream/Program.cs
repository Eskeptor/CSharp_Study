using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace BCL_Stream
{
    class MemoryStreamEx
    {
        public static void Run()
        {
            byte[] shortBytes = BitConverter.GetBytes((short)32000);
            byte[] intBytes = BitConverter.GetBytes(1652300);

            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(shortBytes, 0, shortBytes.Length);
            memoryStream.Write(intBytes, 0, intBytes.Length);
            memoryStream.Position = 0;

            byte[] outBytes = new byte[2];
            memoryStream.Read(outBytes, 0, 2);
            short shortResult = BitConverter.ToInt16(outBytes, 0);
            Console.WriteLine(shortResult);

            outBytes = new byte[4];
            memoryStream.Read(outBytes, 0, 4);
            int intResult = BitConverter.ToInt32(outBytes, 0);
            Console.WriteLine(intResult);
            Console.WriteLine();
            memoryStream.Close();
        }
    }
    class StreamWriterReaderEx
    {
        public static void Run()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            streamWriter.WriteLine("StreamWriter");
            streamWriter.WriteLine("Hello World!");
            streamWriter.Write("C Sharp Study");
            streamWriter.WriteLine("!!!");
            streamWriter.Flush();
            memoryStream.Position = 0;

            StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            string str = streamReader.ReadToEnd();
            Console.WriteLine(str);
            Console.WriteLine();
            streamWriter.Close();
            streamReader.Close();
            memoryStream.Close();
        }
    }
    class BinaryWriterReaderEx
    {
        public static void Run()
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write("BinaryWriter" + Environment.NewLine);
            binaryWriter.Write("Hello World!" + Environment.NewLine);
            binaryWriter.Write("C Sharp Study");
            binaryWriter.Write("!!!" + Environment.NewLine);
            binaryWriter.Flush();
            memoryStream.Position = 0;

            BinaryReader binaryReader = new BinaryReader(memoryStream);
            string str;
            try
            {
                while ((str = binaryReader.ReadString()) != null)
                {
                    Console.Write(str);
                }
            }
            catch(Exception e)
            {
                binaryReader.Close();
                binaryWriter.Close();
                memoryStream.Close();
            }
        }
    }

    [Serializable]
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public Person(int _age, string _name)
        {
            Age = _age;
            Name = _name;
        }

        public Person()
        {
            Age = 0;
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Name + ": " + Age;
        }
    }

    class BinaryFormatterEx
    {
        public static void Run()
        {
            Person person = new Person(13, "홍길동");
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            formatter.Serialize(memoryStream, person);
            memoryStream.Position = 0;

            Person clone = formatter.Deserialize(memoryStream) as Person;
            Console.WriteLine(person);
            Console.WriteLine(clone);
            Console.WriteLine();
            memoryStream.Close();
        }
    }

    class XMLSerializerEx
    {
        public static void Run()
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(Person));
            Person person = new Person(44, "홍길동");
            xml.Serialize(memoryStream, person);

            memoryStream.Position = 0;
            string str = Encoding.UTF8.GetString(memoryStream.ToArray());
            Console.WriteLine("XmlData");
            Console.WriteLine(str);

            memoryStream.Close();
            memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            person = xml.Deserialize(memoryStream) as Person;
            Console.WriteLine(person);
            Console.WriteLine();
            memoryStream.Close();
        }
    }

    class JSONSerializerEx
    {
        public static void Run()
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Person));
            MemoryStream memoryStream = new MemoryStream();
            Person person = new Person(44, "홍길동");
            jsonSerializer.WriteObject(memoryStream, person);

            memoryStream.Position = 0;
            string str = Encoding.UTF8.GetString(memoryStream.ToArray());
            Console.WriteLine("JSONData");
            Console.WriteLine(str);

            memoryStream.Close();
            memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            person = jsonSerializer.ReadObject(memoryStream) as Person;
            Console.WriteLine(person);
            Console.WriteLine();
            memoryStream.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MemoryStreamEx.Run();
            StreamWriterReaderEx.Run();
            BinaryWriterReaderEx.Run();
            BinaryFormatterEx.Run();
            XMLSerializerEx.Run();
            JSONSerializerEx.Run();
        }
    }
}
