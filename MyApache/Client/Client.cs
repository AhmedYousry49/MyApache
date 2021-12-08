using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MyApache
{
    public class Client

    {
        Thread TReceive;//, Tsent;
        private Socket socket;
        byte[] Buffer = new byte[1024];
        public event Action<Client> OnClientConnect, OnClientDisconnect;
        public event Action<byte[], Client> OnClientReceive, OnClientSent;
        public Client(Socket _socket)
        {
            socket = _socket;

            Sent(new byte[0]);
            OnClientReceive += Kernel.Server_OnClientReceive;
            OnClientSent += Kernel.Server_OnClientSent;
            OnClientConnect += Kernel.Server_OnClientConnect;
            OnClientDisconnect += Kernel.Server_OnClientDisconnect;
            if (this.OnClientConnect != null) this.OnClientConnect(this);
            TReceive = new Thread(Receive);
            TReceive.Start();
        }
        public void Sent(byte[] buffer)
        {
            socket.Send(buffer);
           // OnClientSent(buffer, this);
        }
        private void Receive()
        {
            while (true)
            {
                try
                {
                    int size = socket.Receive(Buffer);
                    if (size != 0)
                        if (OnClientReceive != null)
                            OnClientReceive(Buffer, this);
                }
                catch (Exception)
                {
                }

            }
        }
        public string IP
        {
            get
            {
                return (socket.RemoteEndPoint as IPEndPoint).Address.ToString();
            }

        }
        public int port
        {
            get
            {
                return (socket.RemoteEndPoint as IPEndPoint).Port;

            }

        }
      
        public int IPHash
        {
            get
            {
                return IP.GetHashCode();
            }

        }
        public void SReceive()
        {
                TReceive.Abort();
        }
        public void Close()
        {
            socket.Close();
        }
    }
}

