using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace BianCore.Network
{
    public interface TcpSocket
    {
        public Socket socket { set; get; } //套接字
        public bool connected { set; get; } //连接状态
        public Action<TcpSocket> start_action { set; get; }
        public Action<TcpSocket, byte[]> message_action { set; get; }
        public Action<TcpSocket> end_action { set; get; }
        public void MessageService();
        public void Disconnect();
    }
    public class TcpServerSocket : TcpSocket
    {
        public Socket socket { set; get; } //套接字
        public bool connected { set; get; } //连接状态
        public Action<TcpSocket> start_action { set; get; }
        public Action<TcpSocket, byte[]> message_action { set; get; }
        public Action<TcpSocket> end_action { set; get; }
        public static readonly Dictionary<Guid, TcpServerSocket> clients = new();
        private Guid uid;

        public TcpServerSocket(Socket socket, Action<TcpSocket> start_action, Action<TcpSocket, byte[]> message_action, Action<TcpSocket> end_action)
        {
            this.socket = socket;
            this.uid = Guid.NewGuid();
            this.connected = socket.Connected;
            this.start_action = start_action;
            this.message_action = message_action;
            this.end_action = end_action;
        }

        public void MessageService()
        {
            connected = true; //连接状态为正常
            clients.Add(uid, this);
            Trace.WriteLine("[TcpSocketServer]新客户连接建立");
            Task.Run(() => { start_action(this); });
            while (connected)
                try
                {
                    byte[] buffer = new byte[8193];
                    int size = socket.Receive(buffer);
                    if (size <= 0) { throw new Exception(); }
                    Array.Resize(ref buffer, size);
                    Task.Run(() =>
                    { });
                }
                catch { Disconnect(); break; }
            Disconnect();
        }

        public static void BroadcastPacket(string data)
        {
            lock (clients) foreach (var client in clients.Values)
                    try { client.socket.Send(Encoding.UTF8.GetBytes(data)); }
                    catch { client.Disconnect(); }
            Trace.WriteLine($"广播数据：{data}");
        }
        public static void BroadcastPacket(byte[] data)
        {
            lock (clients) foreach (var client in clients.Values)
                    try { client.socket.Send(data); }
                    catch { client.Disconnect(); }
            Trace.WriteLine($"广播数据：{data}");
        }
        public static void SendPacket(TcpServerSocket tcpSocket, string data)
        {
            try { tcpSocket.socket.Send(Encoding.UTF8.GetBytes(data)); }
            catch { tcpSocket.Disconnect(); }
        }
        public static void SendPacket(TcpServerSocket tcpSocket, byte[] data)
        {
            try { tcpSocket.socket.Send(data); }
            catch { tcpSocket.Disconnect(); }
        }
        public void Disconnect()
        {
            if (connected) try
                {
                    socket.Close();
                    lock (clients) clients.Remove(uid);
                    connected = false;
                    Task.Run(() => { end_action(this); });
                    Trace.WriteLine($"连接数发生变化，当前连接数 {clients.Count}");
                }
                catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
        }
    }
    public class TcpClientSocket : TcpSocket
    {
        public Socket socket { set; get; } //套接字
        public bool connected { set; get; } //连接状态
        public Action<TcpSocket> start_action { set; get; }
        public Action<TcpSocket, byte[]> message_action { set; get; }
        public Action<TcpSocket> end_action { set; get; }

        public TcpClientSocket(Socket socket, Action<TcpSocket> start_action, Action<TcpSocket, byte[]> message_action, Action<TcpSocket> end_action)
        {
            this.socket = socket;
            this.connected = socket.Connected;
            this.start_action = start_action;
            this.message_action = message_action;
            this.end_action = end_action;
        }
        public void MessageService()
        {
            Task.Run(() => { start_action(this); });
            if (connected) try
                {
                    Task ReceiveTask = Task.Run(() =>
                    {
                        while (socket != null && connected)
                        {
                            try
                            {
                                // 接收
                                int size = 0;
                                byte[] buffer = new byte[8193];
                                size = socket.Receive(buffer);
                                if (size <= 0) { throw new SocketException(10054); }
                                Array.Resize(ref buffer, size);
                                Task.Run(() => { message_action(this, buffer); });
                            }
                            catch
                            {
                                if (connected) Disconnect();
                                break;
                            }
                        }
                        Disconnect();
                    });
                }
                catch { Disconnect(); }
        }

        public void Disconnect()
        {
            if (connected) try
                {
                    connected = false;
                    socket.Close();
                    Task.Run(() => { end_action(this); });
                    Trace.WriteLine($"客户端已断开连接");
                }
                catch (Exception ex) { Disconnect(); }
        }
    }
}
