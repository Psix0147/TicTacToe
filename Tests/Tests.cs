using System;
using System.Text;
using TicTacToe;
using NUnit.Framework;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestXORCipher()
        {
            var s = Encoding.UTF8.GetBytes("test");
            var m = Encoding.UTF8.GetBytes("mask");
            Assert.AreEqual("test", Client.XORCipher(Encoding.UTF8.GetBytes(Client.XORCipher(s, m)), m));
        }

        [Test]
        public void TestMovePossibility()
        {
            var cells = new[,]
            {
                {'X', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' '},
                {'O', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', ' '}
            };
            Assert.False(Server.IsPossibleMove(3, 0, cells, 3));
            Assert.True(Server.IsPossibleMove(3, 1, cells, 3));
            Assert.False(Server.IsPossibleMove(0, 0, cells, 3));
            Assert.True(Server.IsPossibleMove(1, 1, cells, 3));
        }

        [Test]
        public void TestWinnerCheck()
        {
            var cells = new[,]
            {
                {'X', 'O', 'X', 'X', 'O'},
                {'X', ' ', 'O', 'O', ' '},
                {'X', ' ', 'O', 'O', ' '},
                {'X', 'O', ' ', ' ', 'O'},
                {' ', 'X', 'X', 'X', 'X'}
            };
            char w;
            Assert.True(Server.CheckWinner(0, out w, 0, cells));
            Assert.True(Server.CheckWinner(4, out w, 1, cells));
            Assert.True(Server.CheckWinner(1, out w, 1, cells));
            Assert.True(Server.CheckWinner(24, out w, 0, cells));
            Assert.True(Server.CheckWinner(8, out w, 1, cells));
            Assert.False(Server.CheckWinner(2, out w, 0, cells));
            Assert.False(Server.CheckWinner(15, out w, 1, cells));
        }
    }
}
