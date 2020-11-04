using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;
class start // menu for the snake game (Darrell Lai Vui Kiat)
{
    public void mainmenu()
    {
        string end = "loop";
        do
        {
            string menu1 = @"mainmenu.txt";
            string[] menu = System.IO.File.ReadAllLines(menu1);
            foreach (string line in menu)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t \t \t  " + line);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(40, 16);
            Console.Write("Choice: ");
            Console.ForegroundColor = ConsoleColor.Red;
            ConsoleKeyInfo keyInput = Console.ReadKey();
            if (keyInput.Key == ConsoleKey.D1)
            {
                Console.Clear();
                end = "exitloop";
            }

            else if (keyInput.Key == ConsoleKey.D2)
            {
                var insloop = true;
                while (insloop)
                {
                    Console.Clear();
                    string help1 = @"helppage.txt";
                    string[] help = System.IO.File.ReadAllLines(help1);
                    foreach (string line in help)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t \t \t \t" + line);
                    }
                    Console.SetCursorPosition(57, 13);
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.Enter)
                    {
                        insloop = false;
                        Console.Clear();
                    }
                }
            }
            else if (keyInput.Key == ConsoleKey.D3)
            {
                Console.SetCursorPosition(40, 17);
                System.Environment.Exit(1);
            }
            else
            {
                Console.Clear();
            }
        } while (end == "loop");
    }
}
namespace Snake
{
    struct Position
    {
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string binPath=System.IO.Directory.GetParent(path).ToString();
            string snakePath = System.IO.Directory.GetParent(binPath).ToString();
            //Edited directory for other user to play
            System.Media.SoundPlayer move = new System.Media.SoundPlayer(snakePath+"\\sound\\move.wav");
            System.Media.SoundPlayer eat = new System.Media.SoundPlayer(snakePath+"\\sound\\eat.wav");
            System.Media.SoundPlayer gameover = new System.Media.SoundPlayer(snakePath+"\\sound\\gameover.wav");
            System.Media.SoundPlayer crash = new System.Media.SoundPlayer(snakePath+"\\sound\\crash.wav");
            System.Media.SoundPlayer dying = new System.Media.SoundPlayer(snakePath + "\\sound\\dying.wav");
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            int lastFoodTime = 0;
            int lastFoodTime2 = 0;
            int lastFoodTime3 = 0;
            int snakeLife = 3;
            int userPoints = 0;
            int count1=0;
            int foodDissapearTime = 12000;
            int foodDissapearTime2 = 20000;
            int negativePoints = 0;
            double sleepTime = 100;
            string nametext;
            int direction = right; // To make the snake go to the right when the program starts

            //start menu (Darrell Lai Vui Kiat)
            start start = new start();
            start.mainmenu();
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(2, 5);
                Console.WriteLine("Please Enter your name!:");
                Console.SetCursorPosition(2, 6);
                nametext = Console.ReadLine();
                Console.Clear();
            } while (String.IsNullOrEmpty(nametext));


            Random randomNumbersGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight;
            lastFoodTime = Environment.TickCount;
            lastFoodTime2 = Environment.TickCount;
            lastFoodTime3 = Environment.TickCount;



            // Make sure the snake spawn with just 3 "*"s and spawn it on the top left of the screen

            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i <= 3; i++) //spawn snake body
            {
                snakeElements.Enqueue(new Position(0, i));
            }




            // Spawn the first 5 obstacles in the game 

            List<Position> obstacles = new List<Position>() //spawn the first obstacles
            {
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth)),
            };

            foreach (Position obstacle in obstacles) //write obstacle as "=" on declared position
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.y, obstacle.x);
                Console.Write("=");
            }

            //Food Creation

            Position food;
            do //randomize where the food spawns
            {
                food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth));
            }
            while (snakeElements.Contains(food) || obstacles.Contains(food)); //to make sure that food doesnt spawn on both snake and obstacles

            //Power food creation (Darrell Lai Vui Kiat)
            Position Powerfood;
            do //randomize where the powerfood spawns
            {
                Powerfood = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth));
            }
            while (snakeElements.Contains(Powerfood) || obstacles.Contains(Powerfood));


            //Movement implementation

            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            while (true) //read the direction of arrow key which user inputted
            {
                negativePoints++;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != right) direction = left;
                        move.Play();
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left) direction = right;
                        move.Play();
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = up;
                        move.Play();
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up) direction = down;
                        move.Play();
                    }
                }


                //Creating the snake:

                Position snakeHead = snakeElements.Last(); //make sure the head of the snake is spawned at the end of the "*" position
                Position nextDirection = directions[direction]; //initialize which direction is inputted

                Position snakeNewHead = new Position(snakeHead.x + nextDirection.x,
                                                     snakeHead.y + nextDirection.y); //snakehead will move to the same direction to which the user inputted

                // make sure the snake wont be able to go outside the screen
                if (snakeNewHead.y < 0) snakeNewHead.y = Console.WindowWidth - 1;
                if (snakeNewHead.x < 0) snakeNewHead.x = Console.WindowHeight - 1;
                if (snakeNewHead.x >= Console.WindowHeight) snakeNewHead.x = 0;
                if (snakeNewHead.y >= Console.WindowWidth) snakeNewHead.y = 0;

                foreach (Position position in snakeElements) //writes the body of the snake as "*" on declared position
                {
                    Console.SetCursorPosition(position.y, position.x);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("*");
                }

                //What will happened if the snake got fed:
                if (snakeNewHead.y == food.y && snakeNewHead.x == food.x)
                {
                    // Things that will be happening with the FOOD once it got ate by the snake
                    userPoints += 100;
                    // if user had reach reach a score of 300 it will plus one life
                    if (userPoints == 200)
                    {
                        snakeLife += 1;
                    }
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), //randomize the new position of the food
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) || obstacles.Contains(food)); //writes "@" to indicate food to the designated position it randomized
                    eat.Play();
                    lastFoodTime = Environment.TickCount;
                    sleepTime--;

                    // Things that will be happening with the OBSTACLE once the FOOD got ate by the snake

                    Position obstacle = new Position(); // randomize the position of the obstacles
                    do
                    {
                        obstacle = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(obstacle) ||
                        obstacles.Contains(obstacle) ||
                        (food.x != obstacle.x && food.y != obstacle.y));
                    obstacles.Add(obstacle);
                    Console.SetCursorPosition(obstacle.y, obstacle.x);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("=");
                }
                else
                {
                    // moving...
                    Position last = snakeElements.Dequeue(); // basically moving the snake and delete the last "body part" of the snake to maintain the length of the snake
                    Console.SetCursorPosition(last.y, last.x);
                    Console.Write(" ");
                }

                
                Random rnd = new Random();
                int powcount = rnd.Next(5000, 8000);

                if (Environment.TickCount - lastFoodTime3>powcount && Environment.TickCount - lastFoodTime3 < powcount+1000  )
                {
                    if (count1==0) {
                        Console.SetCursorPosition(Powerfood.y, Powerfood.x);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("$");
                        lastFoodTime2 = Environment.TickCount;
                    }
                   
                }
                //What will happened if the snake got fed on powerfood: (Darrell Lai Vui Kiat)
                if (snakeNewHead.y == Powerfood.y && snakeNewHead.x == Powerfood.x)
                {
                    // Things that will be happening with the FOOD once it got ate by the snake
                    if(count1==0) {
                        userPoints *= 2;
                        Console.SetCursorPosition(Powerfood.y, Powerfood.x);
                        Console.Write(" ");

                        eat.Play();

                        sleepTime--;
                    }
                    count1++;
                   
                }
                // Show and update the score of the player

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(110, 0);
                Console.Write("Score: {0}", userPoints);

                Console.SetCursorPosition(25, 0);
                Console.WriteLine("Remaining Life:" + snakeLife);
                // the game will be over when the snake hits it body or the obstacles
                    
                if (snakeElements.Contains(snakeNewHead) || obstacles.Contains(snakeNewHead))
                    {
                    //Added snake life and dead sound
                    dying.Play();
                    snakeLife--;

                    //add 1 life when user reach 300 points

                    Console.SetCursorPosition(25, 0);
                    Console.WriteLine("Remaining Life:"+snakeLife);
                    if (snakeLife == 0)
                    {
                        gameover.Play();

                        Console.SetCursorPosition(50, 6);


                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Game over!");
                        //if (userPoints < 0) userPoints = 0;

                        Console.SetCursorPosition(45, 7);
                        Console.WriteLine("Your points are: {0}", userPoints);

                        Console.SetCursorPosition(35, 8);
                        Console.WriteLine("Please press the ENTER key to exit the game.");



                        using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(snakePath + "\\score.txt", true))
                        {
                            file.WriteLine(nametext + " - " + userPoints.ToString());
                        }

                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            return;
                        }
                    }
                }

               

                // The game will be over and user will win if they reached 1000 points

                if (userPoints > 1000)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congrats! You've won the game!");
                    Console.WriteLine("Your points are: {0}", userPoints);
                    int loopcount = 0;
                    while (Console.ReadKey().Key != ConsoleKey.Enter) {
                        if (Console.ReadKey().Key == ConsoleKey.Enter) {
                        return;
                        }
                        loopcount =+ 1;
                    }
                }

                // writes the head of the snake as ">","<","^","v" to the position it is declared
                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.y, snakeNewHead.x);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (direction == right) Console.Write(">");
                if (direction == left) Console.Write("<");
                if (direction == up) Console.Write("^");
                if (direction == down) Console.Write("v");
               
                

                // Initialize the time taken for the food to spawn if the snake doesn't eat it

                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    negativePoints = negativePoints + 50;
                    Console.SetCursorPosition(food.y, food.x);
                    Console.Write(" ");
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) || obstacles.Contains(food));
                    lastFoodTime = Environment.TickCount;
                }

                // remove power food after exceeding 20s (Darrell Lai Vui Kiat)
                if (count1==0) {
                    if (Environment.TickCount - lastFoodTime2 >= foodDissapearTime2)
                    {
                        Console.SetCursorPosition(Powerfood.y, Powerfood.x);
                        Console.Write(" ");

                        lastFoodTime2 = Environment.TickCount;
                    }
                }
                

                Console.SetCursorPosition(food.y, food.x);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");

               
                sleepTime -= 0.01;

                Thread.Sleep((int)sleepTime);
            }
        }
    }
}
