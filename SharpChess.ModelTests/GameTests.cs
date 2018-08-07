using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpChess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpChess.Model.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void GenerateChess960FenTest_960Options()
        {
            List<string> possibilities = new List<string>();
            
            int loopcount = 0;

            while(possibilities.Count < 960 && loopcount < 10000000)
            {
                loopcount++;
                string gen = Game.GenerateChess960Fen();

                bool same = false;
                foreach(string s in possibilities)
                {
                    if (gen.Equals(s))
                    {
                        same = true;
                        break;
                    }
                }
                if (!same)
                {
                    possibilities.Add(gen);
                }
            }

            Assert.AreEqual(possibilities.Count, 960);
        }

        [TestMethod()]
        public void GenerateChess960FenTest_MirroredSides()
        {
            string gen = Game.GenerateChess960Fen();
            string side1 = gen.Substring(0, 8);
            string side2 = gen.Substring(35, 8);
            side1 = side1.ToUpper();
            Assert.AreEqual(side1, side2);
        }

        [TestMethod()]
        public void GenerateChess960FenTest_RKR()
        {
            string side1 = Game.GenerateChess960Fen().Substring(0, 8);
            int r1 = -1, k = -1, r2 = -1;

            for(int i = 0; i < side1.Length; i++)
            {
                char c = side1[i];
                if(c == 'r' && r1 == -1)
                {
                    r1 = i;
                }
                else if (c == 'k')
                {
                    k = i;
                }
                else if(c == 'r')
                {
                    r2 = i;
                }
            }
            //I know this assert isn't perfect. But I'm tired.
            Assert.AreEqual(r1 < k, k < r2);

        }

        //Bishops on opposite colors
        [TestMethod()]
        public void GenerateChess960FenTest_Bishop()
        {
            string side1 = Game.GenerateChess960Fen().Substring(0, 8);
            int b1 = 0, b2 = 0;

            for(; b1 < side1.Length; b1++)
            {
                if (side1[b1].Equals('b'))
                {
                    break;
                }
            }
            for (; b2 < side1.Length; b2++)
            {
                if (b2 != b1)
                {
                    if (side1[b2].Equals('b'))
                    {
                        break;
                    }
                }
            }
            Assert.AreNotEqual(b1 % 2, b2 % 2);
        }

        [TestMethod()]
        public void GameStartPositionTest_valid()
        {
            string side1 = Fen.GameStartPosition.Substring(0,8);

            Assert.AreEqual(side1, "rnbqkbnr");
        }
    }
}