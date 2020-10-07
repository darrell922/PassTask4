using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Xml;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
            {
                // start game
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                // display this char on the console during the game
                string ch = "***";
                string blank1 = "   ";
                int score = 0;
                bool gameLive = true;
                ConsoleKeyInfo consoleKey; // holds whatever key is pressed
                int fy = 20, fx = 50;
                int endgame = 0;
            

            // location info & display
            int x = 0, y = 2; // y is 2 to allow the top row for directions & space
            int dx = 1, dy = 0;
                int consoleWidthLimit = 79;
                int consoleHeightLimit = 24;

                // clear to color
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Clear();

                // delay to slow down the character movement so you can see it
                int delayInMillisecs = 100;

                // whether to keep trails
                bool trail = false;
<<<<<<< HEAD
                Console.SetCursorPosition(50, 0);
                Console.Write("Score :"+score);
=======
                
                //spawn starting food edited by Dennis
>>>>>>> 5e505af4f00ddf64dab950eb53d443968fc97d74
                Console.SetCursorPosition(fx, fy);

                Console.Write('x');

            //spawn obstacle edited by Brandon
            Random rnd = new Random();
            int random = rnd.Next(5,22);
            Console.SetCursorPosition(random, random);
            Console.Write("||");


            do // until escape
                {
                    // print directions at top, then restore position
                    // save then restore current color
                    ConsoleColor cc = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Arrows move up/down/right/left. Press 'esc' quit.");
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = cc;

                    // see if a key has been pressed
                    if (Console.KeyAvailable)
                    {
                        // get key and use it to set options
                        consoleKey = Console.ReadKey(true);
                        switch (consoleKey.Key)
                        {

                            case ConsoleKey.UpArrow: //UP
                                dx = 0;
                                dy = -1;
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case ConsoleKey.DownArrow: // DOWN
                                dx = 0;
                                dy = 1;
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case ConsoleKey.LeftArrow: //LEFT
                                dx = -1;
                                dy = 0;
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case ConsoleKey.RightArrow: //RIGHT
                                dx = 1;
                                dy = 0;
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;
                            case ConsoleKey.Escape: //END
                                gameLive = false;
                                break;
                        }
                    }

                    // find the current position in the console grid & erase the character there if don't want to see the trail
                    Console.SetCursorPosition(x, y);
                    if (trail == false)
                        Console.Write(blank1);

                    // calculate the new position
                    // note x set to 0 because we use the whole width, but y set to 1 because we use top row for instructions
                    x += dx;
                    if (x > consoleWidthLimit)
                        x = 0;
                    if (x < 0)
                        x = consoleWidthLimit;

                    y += dy;
                    if (y > consoleHeightLimit)
                        y = 2; // 2 due to top spaces used for directions
                    if (y < 2)
                        y = consoleHeightLimit;

                    //Spawn food edited by Dennis
                    if (y == fy && x == fx)
                    {
                        //Food does not spawn at obstacle edited by Dennis
                        do
                        {
                            fx = rnd.Next(78);

                            fy = rnd.Next(3, 23);

                            Console.SetCursorPosition(fx, fy);

                            Console.Write('x');

                        } while (fx == random && fy == random);
                    endgame++;

<<<<<<< HEAD
                        //snake length increase by darrell

                        ch += "*";
                        blank1 += " ";
                        if (trail == false)
                            Console.Write(blank1);

                        score++;
                        Console.SetCursorPosition(50, 0);
                        Console.Write("Score :" + score);
                    }
=======

                }
>>>>>>> 5e505af4f00ddf64dab950eb53d443968fc97d74

               

                //Spawn obstacle edited by Brandon
                if (y == random && x == random)
                {
                    gameLive = false;
                }
                //Winning Condition edited by Dennis
                if (endgame > 5) {
                    gameLive = false;
                }

                // write the character in the new position
                Console.SetCursorPosition(x, y);
                    Console.Write(ch);

                    // pause to allow eyeballs to keep up
                    System.Threading.Thread.Sleep(delayInMillisecs);

                } while (gameLive);

            Console.Clear();
            Console.SetCursorPosition(50, 10);
<<<<<<< HEAD
            Console.Write(" ========Game Over======== ");
            Console.SetCursorPosition(50, 0);
            Console.Write("Score :" + score);
=======
            Console.Write(" ========Avengers:EndGame!======== ");
>>>>>>> 5e505af4f00ddf64dab950eb53d443968fc97d74
            Console.SetCursorPosition(50, 11);
            Console.WriteLine("Press any key to start playing");
            Console.SetCursorPosition(47, 14);
            Console.WriteLine("Press 'Enter' key to quit the game");
        }
    }

}