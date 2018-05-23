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

        private static Client C;

        public Server(int port)
        {
            var clients = new List<Client>();
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();
            while (clients.Count < 2)
            {
                clients.Add(Client.AcceptClient(Listener));
            }

            Console.WriteLine("\n\nGame Over\n");
        }


        ~Server()
        {
            Listener?.Stop();
        }
    }
}