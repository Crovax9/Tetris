using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

delegate void delegateKey(ConsoleKey type);
namespace Tetris
{
    
    class Program
    {
        private static Stopwatch timer = new Stopwatch();
        private static Stopwatch dropTimer = new Stopwatch();
        static int dropTime, dropRate = 500;
        static Polyomino poly = new Polyomino();
        static Grid grid = new Grid(18, 23);

        private static Thread playerInput;

        private static KeyEvent keyEvent;

        static void Main(string[] args)
        {
            playerInput = new Thread(Init);
            playerInput.Start();

            timer.Start();
            dropTimer.Start();

            grid.DrawBoard();
            poly.Spawn();

            Update();
        }
        
        private static void Init()
        {
            keyEvent = new KeyEvent();
            delegateKey uKey = userKey;
            keyEvent.AddEventListener(uKey);
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
                    poly.BlockClear();
                }
            } //end Update
        }

        private static void userKey(ConsoleKey consoleKey)
        {
            switch (consoleKey)
            {
                case ConsoleKey.UpArrow:
                    poly.BlockRotate();
                    break;
                case ConsoleKey.DownArrow:
                    poly.BlockMove(1, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    poly.BlockMove(0, -2);
                    break;
                case ConsoleKey.RightArrow:
                    poly.BlockMove(0, 2);
                    break;
            }
        }
    }

    
}
