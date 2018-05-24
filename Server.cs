using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace TicTacToe
{
    internal class Server
    {
        private readonly TcpListener Listener;

        public Server(int port)
        {
            var clients = new List<Client>();
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();
            while (clients.Count < 2)
            {
                Client c;
                clients.Add(c = Client.AcceptClient(Listener));
                c.SendMessage(Console.ReadLine());
                Console.WriteLine(c.RecieveMessage());
            }
        }

        ~Server()
        {
            Listener?.Stop();
        }
    }
}