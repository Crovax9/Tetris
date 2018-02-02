using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Polyomino
    {
        private readonly int[,] iMino = new int[1, 4] { { 1, 1, 1, 1 } };
        private readonly int[,] oMino = new int[2, 2] { { 1, 1 }, { 1, 1 } };
        private readonly int[,] tMino = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } };
        private readonly int[,] sMino = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } };
        private readonly int[,] zMino = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };
        private readonly int[,] jMino = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } };
        private readonly int[,] lMino = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };
        public int[,] selectedShape;
        //int[,] location = new int[18, 23];//GameGrid
        int[,] grid = new int[18, 23];
        public List<int[]> location = new List<int[]>();

        public Polyomino()
        {

        }

        public void Spawn()
        {
            Random randomPolyomino = new Random();
            switch (randomPolyomino.Next(0, 7))
            {
                case 0:
                    selectedShape = iMino;
                    break;

                case 1:
                    selectedShape = oMino;
                    break;

                case 2:
                    selectedShape = tMino;
                    break;

                case 3:
                    selectedShape = sMino;
                    break;

                case 4:
                    selectedShape = zMino;
                    break;

                case 5:
                    selectedShape = jMino;
                    break;

                case 6:
                    selectedShape = lMino;
                    break;

                default:

                    break;
            }
            for (int i = 0; i < selectedShape.GetLength(0); i++)
            {
                for (int j = 0; j < selectedShape.GetLength(1); j++)
                {
                    if (selectedShape[i, j] == 1)
                    {
                        Console.SetCursorPosition(((10 - selectedShape.GetLength(1)) / 2 + j) * 2, i);
                        Console.Write("□");
                        location.Add(new int[] { ((10 - selectedShape.GetLength(1)) / 2 + j) * 2, i });

                    }
                }
            }
        }
        
        
        public void AutoDrop()
        {
            if (IsSomthingBelow())
            {
               


            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    location[i][1] += 1;
                }
                for (int i = 0; i < 23; i++)
                {
                    for (int j = 0; j < 18; j++)
                    {
                        grid[j, i] = 0;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    grid[location[i][0], location[i][1]] = 1;
                }
                ReDraw();
            }
        }
        
        void ReDraw()
        {
            for (int i = 0; i < 23; ++i)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.SetCursorPosition(j + 2, i);
                    if (grid[j, i] == 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("□");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

            }
        }

        bool IsSomthingBelow()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (location[i][1] + 1 >= 22)
                    return true;
                if (location[i][1] + 1 < 22)
                {
                    return false;
                }
            }
            return false;

        }
        
    }
}
