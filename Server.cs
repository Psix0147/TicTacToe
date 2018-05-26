using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TicTacToe
{
    internal class Server
    {
        private readonly TcpListener Listener;
        private static char[,] cells;
        private static int turn;

        public Server(int port)
        {
            cells = new char[5, 5];
            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
                cells[i, j] = ' ';

            var gameEnded = false;
            turn = 0;
            var clients = new List<Client>(2);
            var first = new Random().Next() % 2;
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();
            while (clients.Count < 2)
            {
                clients.Add(Client.AcceptClient(Listener));
                Console.WriteLine("Client " + (clients.Count - 1) + " has joined");
            }

            Thread.Sleep(100);
            var queue = new List<Client>
            {
                clients[first],
                clients[(first + 1) % 2]
            };
            Console.WriteLine("Game started!");
            Console.WriteLine("First player is client #" + first);
            for (var i = 0; i < queue.Count; i++)
            {
                queue[i].SendMessage($"msg.You are {(i == 0 ? "first" : "second")} player");
                queue[i].SendMessage($"game.symbol.{(i == 0 ? 'X' : 'O')}");
            }

            var w = ' ';
            while (!gameEnded)
            {
                for (var i = 0; i < 2; i++)
                {
                    var pos = 0;
                    if (queue[i].TcpClient.Available == 0 || i != turn % 2) continue;
                    if (!int.TryParse(queue[i].ReceiveMessage(), out pos)) continue;
                    if (!isPossibleMove(pos, i)) continue;
                    queue[i].SendMessage("move.good");
                    Console.WriteLine($"{(turn % 2 == 0 ? 'X' : 'O')}:{pos}");
                    queue[(i + 1) % 2].SendMessage($"move.place.{pos}.{(turn % 2 == 0 ? 'X' : 'O')}");
                    cells[pos / 5, pos % 5] = turn % 2 == 0 ? 'X' : 'O';
                    if (CheckWinner(pos, out w) && turn < 25)
                    {
                        gameEnded = true;
                    }
                    else turn++;
                }
            }

            foreach (var t in queue)
            {
                t.SendMessage($"msg.{w} wins\nTo play again reload the page.");
                t.SendMessage(opcode: 0x8);
            }

            Listener.Stop();
        }

        private static bool isPossibleMove(int pos, int playerID) => playerID == turn % 2 &&
                                                                     pos > -1 && pos < 25 &&
                                                                     cells[pos / 5, pos % 5] == ' ';

        private static bool CheckWinner(int start, out char w)
        {
            int right = 0, left = 0, up = 0, down = 0, upRight = 0, downRight = 0, upLeft = 0, downLeft = 0;
            w = turn % 2 == 0 ? 'X' : 'O';
            var a = start / 5;
            var b = start % 5;
            for (int i = a, j = b; i < 5; i++)
            {
                if (cells[i, j] == w) down++;
                else break;
            }

            for (int i = a, j = b; i >= 0; i--)
            {
                if (cells[i, j] == w) up++;
                else break;
            }

            for (int i = a, j = b; j < 5; j++)
            {
                if (cells[i, j] == w) right++;
                else break;
            }

            for (int i = a, j = b; j >= 0; j--)
            {
                if (cells[i, j] == w) left++;
                else break;
            }

            for (int i = a, j = b; i >= 0 && j >= 0; i--, j--)
            {
                if (cells[i, j] == w) upLeft++;
                else break;
            }

            for (int i = a, j = b; i >= 0 && j < 5; i--, j++)
            {
                if (cells[i, j] == w) upRight++;
                else break;
            }

            for (int i = start / 5, j = start % 5; i < 5 && j >= 0; i++, j--)
            {
                if (cells[i, j] == w) downLeft++;
                else break;
            }

            for (int i = start / 5, j = start % 5; i < 5 && j < 5; i++, j++)
            {
                if (cells[i, j] == w) downRight++;
                else break;
            }

            return down + up - 1 >= 4 ||
                   left + right - 1 >= 4 ||
                   upRight + downLeft - 1 >= 4 ||
                   upLeft + downRight - 1 >= 4;
        }

        ~Server()
        {
            Listener?.Stop();
        }
    }
}