using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Grid
    {
        private int gridX;
        private int gridY;

        private int[,] grid;

        public int[,] gGrid
        {
            get { return grid; }
        }

        public Grid(int _gridX, int _gridY)
        {
            gridX = _gridX;
            gridY = _gridY;

            grid = new int[gridX, gridY];
        }


        public void DrawBoard()
        {
            string line = "■";
            for (int i = 0; i < gridY; ++i)
            {
                Console.WriteLine(line);
            }
            for (int i = 0; i < gridY; ++i)
            {
                Console.SetCursorPosition(18, i);
                Console.WriteLine(line);
            }
            Console.SetCursorPosition(0, 23);
            for (int i = 0; i < gridX / 2 + 1; ++i)
            {
                Console.Write(line);
            }
            Console.WriteLine();
        }
    }
}
