using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tetris
{
    
    class Program
    {
        private static Stopwatch timer = new Stopwatch();
        private static Stopwatch dropTimer = new Stopwatch();
        static int dropTime, dropRate = 500;
        static Polyomino poly = new Polyomino();
        static Grid grid = new Grid(10, 23);

        static void Main(string[] args)
        {

            timer.Start();
            dropTimer.Start();


            grid.DrawBoard();
            poly.Spawn();

            Update();

        }

        private static void Update()
        {
            while (true)//Update Loop
            {
                dropTime = (int)dropTimer.ElapsedMilliseconds;
                if (dropTime > dropRate)
                {
                    poly.AutoDrop();
                    dropTime = 0;
                    dropTimer.Restart();
                }
            } //end Update
        }
    }

    
}
