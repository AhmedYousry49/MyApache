using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace MyApache.Network
{
    public class PacketHandler
    {
        public static void HandlePacket(byte[] packet, Client client)
        {
            if (packet == null)
                return;
            if (client == null)
                return;
            {

                StringBuilder messageData = new StringBuilder();
                byte[] buffer = new byte[1024];
                int bytes = -1;

           //     Console.WriteLine(messageData.ToString());


                string res  = Encoding.ASCII.GetString(packet);
                Console.WriteLine(res);
              string[] data  = res.Split('\n')[0].Split(' ');

                if (data[1] == "/")
                {
                    string readText = File.ReadAllText("www/index.html");
                    string axs =
$@"HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: {readText.Length}
Content-Type: text/html
Date:{DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:ss GMT")}" +
"\r\n\r\n";
                    client.Sent(Encoding.ASCII.GetBytes(axs));
                    //  Thread.Sleep(200);
                    client.Sent(Encoding.ASCII.GetBytes(readText));
                    client.Close();
                }
                else if (File.Exists("www" + data[1]))
                {
                    string readText = File.ReadAllText("www" + data[1]);
                    string axs =
$@"HTTP/1.1 200 OK
Connection: Keep-Alive
Content-Length: {readText.Length}
Content-Type: text/html
Date:{DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:ss GMT")}
Keep-Alive: timeout=5, max=100
Server: Apache/2.2.8 (Win32) PHP/5.2.6
X-Powered-By: PHP/5.9.6" +
"\r\n\r\n";
                    client.Sent(Encoding.ASCII.GetBytes(axs));
                    client.Sent(Encoding.ASCII.GetBytes(readText));
                }
                else
                {

                    string readText = File.ReadAllText("www/NotFound.html");
                    string axs =
    $@"HTTP/1.1 404 Not Found
Connection: Keep-Alive
Content-Length:  {readText.Length}
Content-Type: text/html
Date:{DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:ss GMT")}
Keep-Alive: timeout=5, max=100
Server: Apache/2.2.8 (Win32) PHP/5.2.6
X-Powered-By: PHP/5.9.6" +
    "\r\n\r\n";
                    client.Sent(Encoding.ASCII.GetBytes(axs));
                    client.Sent(Encoding.ASCII.GetBytes(readText.Replace("192.168.1.40", "192.168.1.113")));
                    client.Close();
                }

               


            }
        }
    }
}
    
