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
        public TcpClient TcpClient;
        public Thread Thread;

        private static void ClientThread(object stateInfo) => Handshake((TcpClient) stateInfo);

        private static void Handshake(TcpClient client)
        {
            var stream = client.GetStream();
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
                                                  "Sec-WebSocket-Extensions: permessage-deflate\r\n\r\n");
            stream.Write(response, 0, response.Length);
        }

        public static Client AcceptClient(TcpListener listener)
        {
            var client = listener.AcceptTcpClient();
            var stream = client.GetStream();

            var html = File.ReadAllText(Environment.CurrentDirectory + "/test.html");
            var str = $"HTTP/1.1\nContent-type: text/html\nContent-Length:{html.Length}\n\n{html}";
            var buffer = Encoding.ASCII.GetBytes(str);
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();

            var thread = new Thread(ClientThread);
            client = listener.AcceptTcpClient();
            thread.Start(client);

            var ret = new Client
            {
                TcpClient = client,
                Thread = thread
            };
            return ret;
        }
    }
}