using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCL_Network
{
    class IPaddressEx
    {
        public static void Run()
        {
            IPAddress ip1 = IPAddress.Parse("123.123.123.123");
            IPAddress ip2 = new IPAddress(new byte[] { 123, 123, 123, 123 });
            Console.WriteLine("ip1: " + ip1);
            Console.WriteLine("ip2: " + ip2);
            Console.WriteLine();
        }
    }
    class DNSEx
    {
        public static void Run()
        {
            string myCom = Dns.GetHostName();
            Console.WriteLine("HostName: " + myCom);
            IPHostEntry entry = Dns.GetHostEntry(myCom);
            foreach(IPAddress ip in entry.AddressList)
            {
                Console.WriteLine(ip.AddressFamily + ": " + ip);
            }
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IPaddressEx.Run();
            DNSEx.Run();
            UDPProgram.UDPServer.Run();
            UDPProgram.UDPClient.Run();
            TCPProgram.TCPServer.Run();
            TCPProgram.TCPClient.Run();
        }
    }
}

namespace NetBase
{
    struct Constant
    {
        public const int UDP_PORT = 15555;
        public const int TCP_PORT = 15550;
        public const int BUFFER = 1024;
    }
}

namespace TCPProgram
{
    class AsyncStateData
    {
        public byte[] buffer;
        public Socket socket;
    }

    class TCPServer
    {
        private static void Server(object _obj)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, NetBase.Constant.TCP_PORT);
                socket.Bind(endPoint);
                socket.Listen(10);


                while (true)
                {
                    Socket clientSocket = socket.Accept();
                    AsyncStateData data = new AsyncStateData();
                    data.buffer = new byte[NetBase.Constant.BUFFER];
                    data.socket = clientSocket;

                    clientSocket.BeginReceive(data.buffer, 0, data.buffer.Length, SocketFlags.None, asyncReceiveCallback, data);
                }
            }
        }

        private static void asyncReceiveCallback(IAsyncResult _async)
        {
            AsyncStateData data = _async.AsyncState as AsyncStateData;
            int nRecv = data.socket.EndReceive(_async);
            string contents = Encoding.UTF8.GetString(data.buffer, 0, nRecv);

            byte[] sendBytes = Encoding.UTF8.GetBytes("SEND:" + contents);
            data.socket.BeginSend(sendBytes, 0, sendBytes.Length, SocketFlags.None, asyncSendCallback, data.socket);
        }

        private static void asyncSendCallback(IAsyncResult _async)
        {
            Socket socket = _async.AsyncState as Socket;
            socket.EndSend(_async);
            socket.Close();
        }

        public static void Run()
        {
            Thread serverThread = new Thread(Server);
            serverThread.IsBackground = true;
            serverThread.Start();
            Console.WriteLine("프로그램 종료");
        }
    }

    class TCPClient
    {
        private static void Client(object _obj)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, NetBase.Constant.TCP_PORT);
                socket.Connect(serverEP);

                int nCount = 5;

                while(nCount-- > 0)
                {
                    
                    byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                    socket.Send(buffer);

                    byte[] receiveBytes = new byte[NetBase.Constant.BUFFER];
                    int nRecv = socket.Receive(receiveBytes);
                    string contents = Encoding.UTF8.GetString(receiveBytes, 0, nRecv);

                    Console.WriteLine(contents);
                    Thread.Sleep(1000);
                }
                
                socket.Close();
                Console.WriteLine("TCP Client Socket: Closed");
            }
        }

        public static void Run()
        {
            Thread.Sleep(500);
            Thread clientThread = new Thread(Client);
            clientThread.IsBackground = true;
            clientThread.Start();
            Thread.Sleep(5500);
            Console.WriteLine();
        }
    }
}

namespace UDPProgram
{
    class UDPServer
    {
        private static IPAddress GetCurrentIP()
        {
            IPAddress[] address = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach(IPAddress ip in address)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;
            }
            return null;
        }

        private static void Server(object _obj)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                //IPAddress ip = GetCurrentIP();
                //if (ip == null) 
                //{
                //    Console.WriteLine("인터넷이 연결되지 않았습니다.");
                //    return;
                //}
                //IPEndPoint endPoint = new IPEndPoint(ip, PORT);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, NetBase.Constant.UDP_PORT);
                socket.Bind(endPoint);

                byte[] receiveBytes = new byte[NetBase.Constant.BUFFER];
                EndPoint clientEP = new IPEndPoint(IPAddress.None, 0);
                while (true)
                {
                    int nRecv = socket.ReceiveFrom(receiveBytes, ref clientEP);
                    string contents = Encoding.UTF8.GetString(receiveBytes, 0, nRecv);
                    byte[] sendBytes = Encoding.UTF8.GetBytes("SEND:" + contents);
                    socket.SendTo(sendBytes, clientEP);
                }
            }
        }

        public static void Run()
        {
            Thread serverThread = new Thread(Server);
            serverThread.IsBackground = true;
            serverThread.Start();
            Console.WriteLine("프로그램 종료");
        }
    }

    class UDPClient
    {
        private static void Client(object _obj)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, NetBase.Constant.UDP_PORT);
                EndPoint clientEP = new IPEndPoint(IPAddress.None, 0);
                int nTimes = 5;

                while(nTimes-- > 0)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                    socket.SendTo(buffer, serverEP);

                    byte[] receiveBytes = new byte[NetBase.Constant.BUFFER];
                    int nRecv = socket.ReceiveFrom(receiveBytes, ref clientEP);
                    string contents = Encoding.UTF8.GetString(receiveBytes, 0, nRecv);

                    Console.WriteLine(contents);
                    Thread.Sleep(1000);
                }
                socket.Close();
                Console.WriteLine("UDP Client Socket: Closed");
                Console.WriteLine();
            }
        }

        public static void Run()
        {
            Thread.Sleep(500);
            Thread clientThread = new Thread(Client);
            clientThread.IsBackground = true;
            clientThread.Start();
            Thread.Sleep(5500);
            Console.WriteLine();
        }
    }
}