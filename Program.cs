using System;
using System.Collections.Generic;

namespace Snake_Game
{
    class Program
    {
        static int boardWidth = 20;
        static int boardHeight = 10;
        static int snakeX = boardWidth / 2;
        static int snakeY = boardHeight / 2;
        static int snakeLength = 1;
        static int score = 0;
        static List<Tuple<int, int>> snakeBody = new List<Tuple<int, int>>();
        static Tuple<int, int> food = new Tuple<int, int>(0, 0);
        static Random random = new Random();

        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;

            snakeBody.Add(new Tuple<int, int>(snakeX, snakeY));

            PlaceFood();

            do
            {
                DrawBoard();

                Console.WriteLine("Score: " + score);

                keyInfo = Console.ReadKey();

                MoveSnake(keyInfo.Key);

                Console.Clear();

            } while (keyInfo.Key != ConsoleKey.Escape);

        }

        static void DrawBoard()
        {
            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    if (snakeBody.Contains(new Tuple<int, int>(x, y)))
                    {
                        Console.Write("O");
                    }
                    else if (x == food.Item1 && y == food.Item2)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }

        static void MoveSnake(ConsoleKey key)
        {
            Tuple<int, int> nextHead = snakeBody[0];

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    nextHead = new Tuple<int, int>(snakeX - 1, snakeY);
                    break;

                case ConsoleKey.RightArrow:
                    nextHead = new Tuple<int, int>(snakeX + 1, snakeY);
                    break;

                case ConsoleKey.UpArrow:
                    nextHead = new Tuple<int, int>(snakeX, snakeY - 1);
                    break;

                case ConsoleKey.DownArrow:
                    nextHead = new Tuple<int, int>(snakeX, snakeY + 1);
                    break;
            }

            if (nextHead.Item1 < 0 || nextHead.Item1 >= boardWidth ||
                nextHead.Item2 < 0 || nextHead.Item2 >= boardHeight ||
                snakeBody.Contains(nextHead))
            {
                // game over
                Console.WriteLine("Game Over");
                Console.WriteLine("Score: " + score);
                Console.ReadKey();
                Environment.Exit(0);
            }

            snakeBody.Insert(0, nextHead);

            if (nextHead.Item1 == food.Item1 && nextHead.Item2 == food.Item2)
            {
                // eat food
                score++;
                snakeLength++;
                PlaceFood();
            }
            else
            {
                // move snake
                snakeBody.RemoveAt(snakeLength);
            }

            snakeX = nextHead.Item1;
            snakeY = nextHead.Item2;
        }
        static void PlaceFood()
        {
            bool validLocation = false;

            while (!validLocation)
            {
                int x = random.Next(0, boardWidth);
                int y = random.Next(0, boardHeight);

                if (!snakeBody.Contains(new Tuple<int, int>(x, y)))
                {
                    food = new Tuple<int, int>(x, y);
                    validLocation = true;
                }
            }
        }

    }
}