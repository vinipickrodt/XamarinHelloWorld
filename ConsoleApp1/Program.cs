using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            A().Wait();
        }

        static async Task A()
        {
            ClientWebSocket clientWebSocket = new ClientWebSocket();
            var uri = new Uri("wss://www.bitmex.com/realtime?subscribe=quote,XBTUSD");
            await clientWebSocket.ConnectAsync(uri, CancellationToken.None);

            do
            {
                var str = await Read(clientWebSocket);
                Console.WriteLine(str);
            } while (true);
        }

        private static async Task<string> Read(ClientWebSocket clientWebSocket)
        {
            var allMsgs = new List<string>();
            WebSocketReceiveResult r = null;

            do
            {
                var msgData = new byte[1024 * 200];
                var msg = new ArraySegment<byte>(msgData);

                r = await clientWebSocket.ReceiveAsync(msg, CancellationToken.None);

                var msgStream = new StreamReader(new MemoryStream(msgData));
                var msgStr = msgStream.ReadToEnd();

                allMsgs.Add(msgStr);
            } while (!r.EndOfMessage);

            return string.Join("", allMsgs);
        }
    }
}
