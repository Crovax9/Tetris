using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class KeyEvent
    {
        public void AddEventListener(delegateKey keyVaule)
        {
            ConsoleKeyInfo keys;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    keys = Console.ReadKey(true);
                    keyVaule(keys.Key);
                }
            }
        }
    }
}
