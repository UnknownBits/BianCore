using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.Network
{
    public class TcpSocket
    {
        public string? username;
        public string? password_sha256;
        public string? email;
        public bool isLogin; //登录状态
        public Socket socket; //套接字
        public bool connected; //连接状态

        public enum PacketType
        {
            Ping,
            PingBack,
            State_Account_Success,
            State_Account_Error,
            State_Server_Closing,
            State_Server_Error,
            Message_Notice,
            Message_Login,
            Message_Register,
            Message_Messages,
        }
        public TcpSocket(Socket socket)
        {
            this.isLogin = false;
            this.socket = socket;
            this.connected = socket.Connected;
        }
    }
    public class TcpServerSocket : TcpSocket
    {
        public static readonly Dictionary<int, TcpServerSocket> clients = new();
        public int UID;
        public TcpServerSocket(Socket socket) : base(socket) { }
        /// <summary>
        /// 广播 type类型 的数据包
        /// </summary>
        /// <param name="type">数据包类型</param>
        public static void BroadcastPacket(PacketType type)
        {
            lock (clients)
                foreach (var client in clients.Values)
                    try { client.socket.Send(new byte[1] { (byte)type }); }
                    catch { client.Disconnect(); }
            Console.WriteLine($"广播 {type} 包");
        }
        /// <summary>
        /// 广播 type类型 data数据 的数据包
        /// </summary>
        /// <param name="type">数据包类型</param>
        /// <param name="data">自定义数据</param>
        public static void BroadcastPacket(PacketType type, string data)
        {
            lock (clients)
                foreach (var client in clients.Values)
                    try { client.socket.Send(new byte[1] { (byte)type }.Concat(Encoding.UTF8.GetBytes(data)).ToArray()); }
                    catch { client.Disconnect(); }
            Console.WriteLine($"广播{type} 包：{data}");
        }
        public static void BroadcastPacket(PacketType type, string data, int UID)
        {
            lock (clients)
                foreach (var client in clients.Values)
                    if (client.UID != UID)
                        try { client.socket.Send(new byte[1] { (byte)type }.Concat(Encoding.UTF8.GetBytes(data)).ToArray()); }
                        catch { client.Disconnect(); }
            Console.WriteLine($"广播{type} 包：{data}");
        }
        public static void SendPacket(PacketType type, int UID)
        {
            foreach (var client in clients.Values)
                if (client.UID == UID)
                    try { client.socket.Send(new byte[1] { (byte)type }); }
                    catch { client.Disconnect(); }
            Console.WriteLine($"发送 {type} 包");
        }
        public static void SendPacket(PacketType type, int UID, string data)
        {
            foreach (var client in clients.Values)
                if (client.UID == UID)
                    try { client.socket.Send(new byte[1] { (byte)type }.Concat(Encoding.UTF8.GetBytes(data)).ToArray()); }
                    catch { client.Disconnect(); }
            Console.WriteLine($"{type} 包：{data}");
        }
        public static void SendPacket(PacketType type, int UID, byte[] data)
        {
            foreach (var client in clients.Values)
                if (client.UID == UID)
                    try { client.socket.Send(new byte[1] { (byte)type }.Concat(data).ToArray()); }
                    catch { client.Disconnect(); }
            Console.WriteLine($"{type} 包：{data}");
        }
        public static void SendPacket(PacketType type, TcpServerSocket tcpSocket)
        {
            try { tcpSocket.socket.Send(new byte[1] { (byte)type }); }
            catch { tcpSocket.Disconnect(); }
            Console.WriteLine($"发送 {type} 包");
        }
        public static void SendPacket(PacketType type, TcpServerSocket tcpSocket, string data)
        {
            try { tcpSocket.socket.Send(new byte[1] { (byte)type }.Concat(Encoding.UTF8.GetBytes(data)).ToArray()); }
            catch { tcpSocket.Disconnect(); }
            Console.WriteLine($"{type} 包：{data}");
        }
        public static void SendPacket(PacketType type, TcpServerSocket tcpSocket, byte[] data)
        {
            try { tcpSocket.socket.Send(new byte[1] { (byte)type }.Concat(data).ToArray()); }
            catch { tcpSocket.Disconnect(); }
            Console.WriteLine($"{type} 包：{data}");
        }
        public void Disconnect()
        {
            if (connected)
                try
                {
                    if (isLogin)
                    {
                        lock (clients) clients.Remove(UID);
                        isLogin = false;
                        BroadcastPacket(PacketType.Message_Notice, $"{username} 已下线");
                    }
                    connected = false;
                    socket.Close();
                    Console.WriteLine($"连接数发生变化，当前连接数 {clients.Count}");
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
    public class TcpClient : TcpSocket
    {
        public event EventHandler<PackageReceive_EventArgs> PackageReceive = delegate { };
        public class PackageReceive_EventArgs : EventArgs
        {
            public PacketType PacketType { get; set; }
            public byte[] Data { get; set; }
        }

        public event EventHandler<PingPackageReceive_EventArgs> PingPackageReceive = delegate { };
        public class PingPackageReceive_EventArgs : EventArgs
        {
            public int Ping { get; set; }
        }
        public event EventHandler<ErrorReceive_EventArgs> ErrorReceive = delegate { };
        public class ErrorReceive_EventArgs : EventArgs
        {
            public Exception Exception { get; set; }
        }

        public event EventHandler<LoginCompletedEventArgs> LoginCompleted = delegate { };
        public class LoginCompletedEventArgs : EventArgs
        {
            public PacketType LoginState { get; set; }
        }

        public TcpClient(Socket socket) : base(socket) { }
        public void Dispose(Exception exception)
        {
            try
            {
                if (connected)
                {
                    connected = false;
                    isLogin = false;
                    username = "";
                    socket.Close();
                }
                Trace.WriteLine($"客户端已断开连接");
                Task.Run(() => ErrorReceive(this, new ErrorReceive_EventArgs { Exception = exception }));

            }
            catch (Exception ex) { Dispose(ex); }
        }
    }
}
