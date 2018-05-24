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
            var gameEnded = false;
            var turn = 0;
            var clients = new List<Client>(2);
            var first = new Random().Next(0, 1);
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();
            while (clients.Count < 2)
            {
                clients.Add(Client.AcceptClient(Listener));
            }

            var queue = new List<Client>
            {
                clients[first],
                clients[Math.Abs((first + 1) % 2)]
            };
            Console.WriteLine("Game started!");
            Console.WriteLine("First player's ID:" + first);
            for (var i = 0; i < 2; i++)
                clients[i].SendMessage($"You are {(first == i ? "first" : "second")} player");

            while (!gameEnded)
            {
                var pos = queue[turn % 2].ReceiveMessage();
                Console.WriteLine($"{(turn % 2 == 0 ? "X" : "O")}:{pos}");
                queue[(turn + 1) % 2].SendMessage($"{(turn % 2 == 0 ? "X" : "O")}:{pos}");
                turn++;
            }
        }

        ~Server()
        {
            Listener?.Stop();
        }
    }
}