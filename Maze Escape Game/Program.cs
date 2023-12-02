namespace Maze_Escape_Game
{
    internal class Program
    {
        static char[,] maze;
        static int playerX = -1;
        static int playerY = -1;

        static int playerXExit = -1;
        static int playerYExit = -1;

        static int steps = 0;

        static int num;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Maze Escape Challenge!\nGenerated Maze:");
            generateMaze();
            DisplayMaze();
            Console.WriteLine();
            StartGame();

        }

        public static void generateMaze()
        {
            Random rand = new Random();
            num = rand.Next(7, 11);
            maze = new char[num, num];

            int rows = num - 1;
            int columns = num - 2;

            maze[1, 1] = 'S';


            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (maze[i, j] != 'S' && maze[i, j] != 'E' && i == 0 || j == 0 || i == num - 1 || j == num - 1) maze[i, j] = '#';
                    else if (maze[i, j] != 'S' && maze[i, j] != 'E') maze[i, j] = '.';
                }
            }
            maze[rows, columns] = 'E';

            int loopTime = 0;
            switch (num)
            {
                case 7: loopTime = 10; break;
                case 8: loopTime = 14; break;
                case 9: loopTime = 16; break;
                case 10: loopTime = 18; break;

                default: loopTime = 5; break;

            }

            for (int i = 0; i < loopTime; i++)
            {
                int iRan = rand.Next(2, num - 2);
                int jRan = rand.Next(1, num - 2);
                if (maze[iRan, jRan] != 'S' && maze[iRan, jRan] != 'E') maze[iRan, jRan] = '#';
            }
        }

        public static void FindStartingPoint()
        {
            Console.WriteLine("Use W, A, S, D to move. Your goal is to reach the Exit (E)!");

            int rows = maze.GetLength(0);
            int columns = maze.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (maze[i, j] == 'S')
                    {
                        playerX = i;
                        playerY = j;
                    }

        }

        public static void FindEndingPoint()
        {
            int rows = maze.GetLength(0);
            int columns = maze.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (maze[i, j] == 'E')
                    {
                        playerXExit = i;
                        playerYExit = j;
                    }
        }


        public static void DisplayMaze()
        {
            int rows = maze.GetLength(0);
            int columns = maze.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == playerX && j == playerY)
                    {
                        Console.Write("S "); // Player position.
                    }
                    else
                    {
                        Console.Write(maze[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            
        }

        public static void StartGame()
        {
            FindStartingPoint();
            FindEndingPoint();

            if (playerX == -1 || playerY == -1) 
                Console.WriteLine("Starting point not found in the maze.");

            
            while (true)
            {

                Console.WriteLine($"Current Position: ({playerX},{playerY})");

                DisplayMaze();
                Console.WriteLine();

                Console.Write("Enter your move (U/L/D/R): ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();
                
                bool changValue = MovePlayer(move);
                
                // Move the player based on the input.
                if (!changValue)
                {
                    Console.WriteLine("Invalid move. Try again.\n");

                }

                // Check if the player has reached the exit.
                
                if (maze[playerXExit, playerYExit] == 'S')
                {

                    DisplayMaze();
                    Console.WriteLine($"\nCongratulations! You've reached the Exit (E) in {steps} moves! ");
                    playerX = -1;
                    playerY = -1;
                    steps = 0;

                    String playAgain;
                    do
                    {
                        Console.Write($"\nDo you want to play again? (Y/N): ");
                        playAgain = Console.ReadLine().ToLower().Trim();
                        
                    } while (!(playAgain == "yes" || playAgain == "no" || playAgain == "n" || playAgain == "y"));

                    if (playAgain == "yes" || playAgain == "y")
                    {
                        generateMaze();

                        DisplayMaze();
                        Console.WriteLine();

                        StartGame();
                    }
                    else
                    {
                        Console.WriteLine("Thank you for playing the Maze Escape Challenge!");
                    }
                    break;                                  
                }
            }
        }

        public static bool MovePlayer(char move)
        {
            maze[playerX, playerY] = '.';
            int newX = playerX;
            int newY = playerY;

            switch (move)
            {
                case 'W':
                    newX--; steps++;
                    break;
                case 'A':
                    newY--; steps++;
                    break;
                case 'S':
                    newX++; steps++;
                    break;
                case 'D':
                    newY++; steps++;
                    break;
                default:
                    return false;
            }

            // Check if the new position is within the maze boundaries and not a wall.
            if (newX >= 0 && newX < maze.GetLength(0) && newY >= 0 && newY < maze.GetLength(1) && maze[newX, newY] != '#')
            {
                playerX = newX;
                playerY = newY;

                maze[playerX, playerY] = 'S';

                return true;
            }

            return false;
        }

    }
}