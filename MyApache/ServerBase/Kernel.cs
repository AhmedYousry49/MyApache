using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyApache.Network;
namespace MyApache
{
    public class Kernel
    {

        static ServerSocket ServerHttp;
        public static void Server_OnClientConnect(Client Client)
        {

        }
        public static void Server_OnClientDisconnect(Client Client)
        {
            Client.SReceive();
            Client.Close();
        }
        public static void AuthServer_OnClientClosing()
        {
            ServerHttp.Disconnect();
        }

        public static void Server_OnClientReceive(byte[] buffer, Client Client)
        {
            PacketHandler.HandlePacket(buffer, Client);
        }
        public static void Server_OnClientSent(byte[] buffer, Client Client)
        {
            Client.Sent(buffer);
        }

     static string ip {
        
        get { return "192.168.1.113"; }
        }
        public static void StartServer()
        {
            ServerHttp = new ServerSocket("192.168.1.113", 80);
        }
        public static ConcurrentDictionary<int, Client> Sate = new ConcurrentDictionary<int, Client>();

       // internal static List<Session> Session = new List<Session>();
       // internal static List<SessionPlan> SessionPlan = new List<SessionPlan>();
       // internal static List<Sessiontele> Sessiontele = new List<Sessiontele>();
    }
}
