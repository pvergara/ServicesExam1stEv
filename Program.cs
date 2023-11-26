using System.Collections.Specialized;

namespace ServicesExam1aEv
{
    internal abstract class Program
    {
        private static readonly object LockKey = new();
        private static int enemyX = 0;
        private static int mainCharacterX = 0;

        private static int mainCharacterSign = 1;
        private static int sign = 1;
        private static Thread shot;
        private static bool thisIsTheEnd = false;

        private static void Main()
        {
            var thread = new Thread(WriteDown);
            thread.Start();
            shot = null;
            lock (LockKey)
            {
                Console.SetCursorPosition(1 + mainCharacterX, 20);
                Console.Write(" |-^-| ");
            }
            var i = 1;
            while (!thisIsTheEnd)
            {
                if (Console.KeyAvailable) //if there’s a key in keyboard’s buffer
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if(key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow){
                        if (key.Key == ConsoleKey.LeftArrow)
                            mainCharacterSign = -1;

                        if (key.Key == ConsoleKey.RightArrow)
                            mainCharacterSign = 1;
                        mainCharacterX += (1 * mainCharacterSign);
                        if (mainCharacterX > 30)
                            mainCharacterX = 30;
                        if (mainCharacterX < 1)
                            mainCharacterX = 0;



                        lock (LockKey)
                        {
                            Console.SetCursorPosition(1 + mainCharacterX, 20);
                            Console.Write(" |-^-| ");
                        }
                    }

                    if (key.Key == ConsoleKey.Spacebar && shot == null)
                    {
                        shot = new Thread(projectile);
                        shot.Start();
                    }


                }

            }

        }

        private static void projectile()
        {
            var i = 0;
            var initialValue = 19;
            var finalHeight = false;
            var characterX = 1 + mainCharacterX + 3;
            while (!finalHeight)
            {
                Thread.Sleep(50);
                lock (LockKey)
                {
                    if (i > 0)
                    {
                        Console.SetCursorPosition(characterX, (initialValue - i + 1));
                        Console.Write(" ");
                    }

                    Console.SetCursorPosition(characterX, (initialValue - i));
                    Console.Write("*");

                }


                if (initialValue - i == 1)
                {
                    finalHeight = true;
                }
                i++;
            }

            if (characterX >= enemyX + 1 && characterX <= enemyX + 6)
            {
                lock (LockKey)
                {
                    thisIsTheEnd = true;
                }
            }
            lock (LockKey)
            {
                Console.SetCursorPosition(characterX, 1);
                Console.Write(" ");
            }

            shot = null;
        }

        private static void WriteDown()
        {
            var i = 1;
            while (!thisIsTheEnd)
            {
                if (i % 30 == 0)
                    sign *= -1;

                enemyX += (1 * sign);
                Thread.Sleep(new Random().Next(200));
                lock (LockKey)
                {
                    Console.SetCursorPosition(1 + enemyX, 1);
                    Console.Write(" Oo0oO ");
                }
                i++;
            }
        }
    }

}
