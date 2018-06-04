using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TicTacToe
{
    public class Server
    {
        private readonly TcpListener Listener;
        private static char[,] cells;
        private static int turn;

        private const string HelpMessage =
            "msg\0/help - show this message\n" +
            "/restart - start voting for a game restart\n" +
            "/giveup - give up";

        public Server(int port)
        {
            cells = new char[5, 5];
            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
                cells[i, j] = ' ';
            var restart = false;
            var gameEnded = false;
            turn = 0;
            var clients = new List<Client>(2);
            var first = new Random().Next() % 2;
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();


            Listener.BeginAcceptTcpClient(Client.SendPageAsync, Listener);
            Listener.BeginAcceptTcpClient(Client.SendPageAsync, Listener);
            Listener.BeginAcceptTcpClient(Client.AcceptClientAsync, Listener);
            Listener.BeginAcceptTcpClient(Client.AcceptClientAsync, Listener);

            while (clients.Count < 2)
            {
                if (Client.staticClient == null) continue;
                clients.Add(Client.staticClient);
                Console.WriteLine(clients[clients.Count - 1].Nickname + " has joined");
                Client.staticClient = null;
            }

            Thread.Sleep(100);
            var queue = new List<Client>
            {
                clients[first],
                clients[(first + 1) % 2]
            };
            Console.WriteLine("Game started!");
            Console.WriteLine($"First player is {clients[first].Nickname}");
            for (var i = 0; i < queue.Count; i++)
            {
                queue[i].SendMessage($"msg\0You are {(i == 0 ? "first" : "second")} player");
                queue[i].SendMessage($"game\0symbol\0{(i == 0 ? 'X' : 'O')}");
            }

            while (!gameEnded)
            {
                for (var i = 0; i < 2; i++)
                {
                    if (queue[i].TcpClient.Available == 0) continue;
                    var msg = queue[i].ReceiveMessage();
                    if (msg[0] == '/')
                    {
                        switch (msg)
                        {
                            case "/help":
                                queue[i].SendMessage(HelpMessage);
                                break;
                            case "/restart":
                                queue[(i + 1) % 2].SendMessage("msg\0" + queue[i].Nickname +
                                                               " wants to restart the game" +
                                                               " (type /restart to restart)");
                                queue[i].SendMessage("msg\0Waiting for opponent...");
                                while (queue[(i + 1) % 2].TcpClient.Available == 0) ;
                                if (queue[(i + 1) % 2].ReceiveMessage() == "/restart")
                                {
                                    gameEnded = true;
                                    restart = true;
                                    foreach (var t in queue)
                                    {
                                        t.SendMessage("msg\0To restart reload the page");
                                    }
                                }

                                break;
                            case "/giveup":
                                queue[i].SendMessage("game\0close");
                                queue[(i + 1) % 2].SendMessage("msg\0" + queue[i].Nickname + " is giving up");
                                turn = (i + 1) % 2;
                                gameEnded = true;
                                break;
                        }
                    }
                    else if (msg[0] == '#')
                    {
                        var m = msg.Substring(1);
                        int pos;
                        if (i != turn % 2 || !int.TryParse(m, out pos)) continue;
                        if (!IsPossibleMove(pos, i)) continue;
                        queue[i].SendMessage("move\0good");
                        Console.WriteLine($"{(turn % 2 == 0 ? 'X' : 'O')}:{pos}");
                        queue[(i + 1) % 2].SendMessage($"move\0place\0{pos}\0{(turn % 2 == 0 ? 'X' : 'O')}");
                        cells[pos / 5, pos % 5] = turn % 2 == 0 ? 'X' : 'O';
                        char w;
                        if (CheckWinner(pos, out w) && turn < 25)
                        {
                            gameEnded = true;
                        }
                        else turn++;
                    }
                    else
                    {
                        foreach (var t in queue)
                        {
                            t.SendMessage("msg\0" + queue[i].Nickname + ": " + msg);
                        }
                    }
                }
            }

            foreach (var t in queue)
            {
                if (!restart)
                    t.SendMessage($"msg\0{clients[turn % 2].Nickname} wins\nIf you want to play again reload the page");
                t.SendMessage(opcode: 0x8);
                t.TcpClient.Close();
            }

            Listener.Stop();
        }

        public static bool IsPossibleMove(int pos, int playerId, char[,] c = null, int t = -1)
        {
            if (c == null) c = cells;
            if (t == -1) t = turn;
            return playerId == t % 2 && pos > -1 && pos < 25 && c[pos / 5, pos % 5] == ' ';
        }

        public static bool CheckWinner(int start, out char w, int t = -1, char[,] c = null)
        {
            int right = 0, left = 0, up = 0, down = 0, upRight = 0, downRight = 0, upLeft = 0, downLeft = 0;
            if (c == null) c = cells;
            if (t == -1) t = turn;
            w = t % 2 == 0 ? 'X' : 'O';
            var a = start / 5;
            var b = start % 5;
            for (int i = a, j = b; i < 5; i++)
            {
                if (c[i, j] == w) down++;
                else break;
            }

            for (int i = a, j = b; i >= 0; i--)
            {
                if (c[i, j] == w) up++;
                else break;
            }

            for (int i = a, j = b; j < 5; j++)
            {
                if (c[i, j] == w) right++;
                else break;
            }

            for (int i = a, j = b; j >= 0; j--)
            {
                if (c[i, j] == w) left++;
                else break;
            }

            for (int i = a, j = b; i >= 0 && j >= 0; i--, j--)
            {
                if (c[i, j] == w) upLeft++;
                else break;
            }

            for (int i = a, j = b; i >= 0 && j < 5; i--, j++)
            {
                if (c[i, j] == w) upRight++;
                else break;
            }

            for (int i = a, j = b; i < 5 && j >= 0; i++, j--)
            {
                if (c[i, j] == w) downLeft++;
                else break;
            }

            for (int i = a, j = b; i < 5 && j < 5; i++, j++)
            {
                if (c[i, j] == w) downRight++;
                else break;
            }

            return down + up - 1 >= 4 ||
                   left + right - 1 >= 4 ||
                   upRight + downLeft - 1 >= 4 ||
                   upLeft + downRight - 1 >= 4;
        }
    }
}