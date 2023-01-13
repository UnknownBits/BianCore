using System.Net.Sockets;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
namespace BianCore.Modules.BianNetwork
{
    public class Tcp
    {
        public string Listen(Int32 port){
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAddr,port);
            try
            {
                listener.Start();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return null;
        }
    }
}
