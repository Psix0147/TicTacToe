using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace TicTacToe
{
    public class Client
    {
        public TcpClient TcpClient { get; private set; }
        private Thread Thread;
        public string Nickname;
        public static Client staticClient;

        private static void ClientThread(object stateInfo) => Handshake((TcpClient) stateInfo);

        private static void Handshake(TcpClient client)
        {
            var stream = client.GetStream();
            while (client.Available == 0) ;
            var bytes = new byte[client.Available];
            stream.Read(bytes, 0, bytes.Length);
            var data = Encoding.UTF8.GetString(bytes);

            var key = new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim();
            if (!new Regex("^GET").IsMatch(data)) return;
            var hash = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(
                key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")));
            var response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols\r\n" +
                                                  "Connection: keep-alive, Upgrade\r\n" +
                                                  "Upgrade: websocket\r\n" +
                                                  $"Sec-WebSocket-Accept: {hash}\r\n" +
                                                  "Sec-WebSocket-Extensions:\r\n\r\n");
            stream.Write(response, 0, response.Length);
        }

        private static void SendPage(TcpClient client)
        {
            var stream = client.GetStream();

            while (client.Available == 0) ;
            var bytes = new byte[client.Available];
            stream.Read(bytes, 0, bytes.Length);
            var html = File.ReadAllText(Environment.CurrentDirectory + "/html.html");
            var str = $"HTTP/1.1\nContent-type: text/html\nContent-Length: {html.Length}\n\n{html}";
            var buffer = Encoding.UTF8.GetBytes(str);
            stream.Write(buffer, 0, buffer.Length);
            client.Close();
        }

        public static void AcceptClientAsync(IAsyncResult ar)
        {
            var l = (TcpListener) ar.AsyncState;
            staticClient = AcceptClient(l.EndAcceptTcpClient(ar));
        }

        public static void SendPageAsync(IAsyncResult ar)
        {
            var l = (TcpListener) ar.AsyncState;
            SendPage(l.EndAcceptTcpClient(ar));
        }

        private static Client AcceptClient(TcpClient client)
        {
            var thread = new Thread(ClientThread);
            thread.Start(client);
            var ret = new Client
            {
                TcpClient = client,
                Thread = thread
            };
            Thread.Sleep(100);
            ret.Nickname = ret.ReceiveMessage();
            return ret;
        }

        public void SendMessage(string msg = "", byte opcode = 0x1)
        {
            var data = Encoding.UTF8.GetBytes(msg);
            var b1 = (byte) (128 + opcode);
            var b2 = (byte) data.Length;
            var tosend = new byte[data.Length + 2];
            tosend[0] = b1;
            tosend[1] = b2;
            for (var i = 0; i < data.Length; i++)
                tosend[i + 2] = data[i];

            TcpClient.GetStream().Write(tosend, 0, tosend.Length);
        }

        public string ReceiveMessage()
        {
            while (TcpClient.Available == 0) ;
            var header = new byte[2];
            TcpClient.GetStream().Read(header, 0, header.Length);
            var mask = new byte[4];
            TcpClient.GetStream().Read(mask, 0, mask.Length);
            var length = header[1] & 127;
            var data = new byte[length];
            TcpClient.GetStream().Read(data, 0, length);
            return XORCipher(data, mask);
        }

        public static string XORCipher(byte[] data, byte[] mask)
        {
            for (var i = 0; i < data.Length; i++)
            {
                data[i] ^= mask[i % 4];
            }
            return Encoding.UTF8.GetString(data);
        }

    }
}