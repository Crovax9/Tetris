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
        int[,] grid = new int[18, 23];
        int[,] fillGrid = new int[18, 23];
        public List<int[]> location = new List<int[]>();

        private bool rotateDirection = false;


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
                        location.Add(new int[] { ((10 - selectedShape.GetLength(1)) / 2 + j) * 2, i});

                    }
                }
            }
        }
        
        
        public void AutoDrop()
        {
            if (IsSomthingBelow())
            {
                for (int i = 0; i < 4; ++i)
                {
                    fillGrid[location[i][0], location[i][1]] = 1;
                }
                location.Clear();
                Spawn();
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

        public void BlockRotate()
        {
            List<int[]> templocation = new List<int[]>();
            for (int i = 0; i < selectedShape.GetLength(0); i++)
            {
                for (int j = 0; j < selectedShape.GetLength(1); j++)
                {
                    if (selectedShape[i, j] == 1)
                    {
                        templocation.Add(new int[] { ((10 - selectedShape.GetLength(1)) / 2 + j) * 2, i });
                    }
                }
            }

            if (selectedShape == oMino)
            {
                return;
            }
            if (selectedShape == iMino)
            {
                if (rotateDirection == false)
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i] = TransformMatrix(location[i], location[2], "Clockwise");
                    }
                    rotateDirection = true;
                }
                else
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i] = TransformMatrix(location[i], location[2], "Counterclockwise");
                    }
                    rotateDirection = false;
                }
            }
            else if (selectedShape == sMino)
            {
                for (int i = 0; i < location.Count; i++)
                {
                    templocation[i] = TransformMatrix(location[i], location[3], "Counterclockwise");
                }
            }
            else
            {
                for (int i = 0; i < location.Count; i++)
                {
                    templocation[i] = TransformMatrix(location[i], location[2], "Clockwise");
                }
            }

            for (int i = 0; i < 4; ++i)
            {
                if (templocation[i][0] < 2)
                {
                    if (selectedShape == iMino)
                    {
                        for (int j = 0; j < 4; ++j)
                            templocation[j][0] += 4;
                    }
                    else
                    {
                        for (int j = 0; j < 4; ++j)
                            templocation[j][0] += 2;
                    }
                    
                }
                else if (templocation[i][0] > 16)
                {
                    for (int j = 0; j < 4; ++j)
                        templocation[j][0] -= 2;
                }
            }
            location = templocation;
        }



        public int[] TransformMatrix(int[] coord, int[] axis, string dir)
        {
            int[] pcoord = { coord[0] - axis[0], coord[1] - axis[1] };
            if (dir == "Counterclockwise")
            {
                pcoord = new int[] { -pcoord[1] * 2, pcoord[0] / 2 };
            }
            else if (dir == "Clockwise")
            {
                pcoord = new int[] { pcoord[1] * 2, -pcoord[0] / 2 };
            }
            return new int[] { pcoord[0] + axis[0], pcoord[1] + axis[1] };
        }

        public void BlockMove(int vertical, int horizontal)
        {
            if (IsSomthingBelow())
            {
                for (int i = 0; i < 4; ++i)
                {
                    fillGrid[location[i][0], location[i][1]] = 1;
                }
                location.Clear();
                Spawn();
            }
            else
            {
                for (int i = 0; i < 4; ++i)
                {
                    location[i][1] += vertical;
                    location[i][0] += horizontal;
                    if (location[i][0] < 2)
                    {
                        for (int j = 0; j < 4; ++j)
                            location[j][0] += 2;
                    }
                    else if (location[i][0] > 16)
                    {
                        for (int j = 0; j < 4; ++j)
                            location[j][0] -= 2;
                    }
                }
            }
        }
        
        void ReDraw()
        {
            for (int i = 0; i < 23; ++i)
            {
                for (int j = 0; j < 18; j++)
                {
                    Console.SetCursorPosition(j + 2, i);
                    if (grid[j, i] == 1 | fillGrid[j, i] == 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("□");
                    }
                    else
                    {
                        if (j < 16)
                        {
                            Console.Write(" ");
                        }
                    }
                }
            }
        }

        bool IsSomthingBelow()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (location[i][1] + 1 >= 23)
                    return true;
                if (location[i][1] + 1 < 23)
                {
                    if (fillGrid[location[i][0], location[i][1] + 1] == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void BlockClear()
        {
            int[,] tempGrid = new int[18, 23];
            for (int i = 0; i < 23; ++i)
            {
                int j = 2;
                for (j = 2; j < 18; j += 2)
                {
                    if (fillGrid[j, i] == 0)
                    {
                        break;
                    }
                }
                if (j == 18)
                {
                    for (j = 2; j < 18; j += 2)
                    {
                        fillGrid[j, i] = 0;
                    }
                    for (int k = 1; k < i; ++k)
                    {
                        for (int l = 0; l < 18; l += 2)
                        {
                            tempGrid[l, k + 1] = fillGrid[l, k];
                        }
                    }
                    for (int k = 1; k < i; ++k)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            fillGrid[l, k] = 0;
                        }
                    }
                    for (int k = 0; k < 23; ++k)
                        for (int l = 0; l < 18; l += 2)
                            if (tempGrid[l, k] == 1)
                                fillGrid[l, k] = 1;
                }
            }
            ReDraw();
        }
    }
}
