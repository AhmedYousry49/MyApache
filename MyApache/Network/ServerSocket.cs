using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace MyApache.Network
{
    class ServerSocket
    {

        private byte[] Buffer = new byte[1024];
        private Socket Connection;
        private ushort port;
        private string ipString;
        private Thread thread;

        private void doSyncAccept()
        {
            while (true)
            {
                try
                {
                    processSocket(this.Connection.Accept());
                }
                catch { }
                Thread.Sleep(1);
            }
        }
        public void Disconnect()
        {
            if (thread.IsAlive)
                thread.Abort();
            lock (Connection)
            {
                shutdown(Connection.Handle, ShutDownFlags.SD_BOTH);
                closesocket(Connection.Handle);
                Connection.Dispose();
                Connection.Close();
            }
        }
        private void processSocket(Socket socket)
        {
            try
            {
                
                Client client = new Client(socket);
                //              Kernel.Sate[Client.id] = Client;
               Console.WriteLine(client.IP + " is Connect port"+ client.port);
           //   Console.WriteLine(client. + " is Connect");
            }
            catch
            {

            }
        }
        public ServerSocket(string ip, ushort port)
        {
            this.Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); this.ipString = ip;
            Enable(port, ip);
        }
        public void Enable(ushort port, string ip)
        {
            this.ipString = ip;
            this.port = port;
            this.Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connection.Bind(new IPEndPoint(IPAddress.Parse(ipString), this.port));
            this.Connection.Listen((int)SocketOptionName.MaxConnections);
            thread = new Thread(doSyncAccept);
            thread.Start();
        }
        public void InvokeDisconnect(ServerSocket Client)
        {
            // if (this.OnClientDisconnect != null)
            //   this.OnClientDisconnect(Client);
        }
        [DllImport("ws2_32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int closesocket(IntPtr s);
        [DllImport("ws2_32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int shutdown(IntPtr s, ShutDownFlags how);
        public enum ShutDownFlags : int
        {
            SD_RECEIVE = 0,
            SD_SEND = 1,
            SD_BOTH = 2
        }
    }
}
