using static System.Console;

int destroyers = 2;
int submarines = 2;
int carriers = 1;
int total = 5;

int hitCountPlayer = 0;
int hitCountEnemy = 0;

bool playerTurn = true;


int enemyTotal = 5;

char[,] grid = new char[8,8];
char[,] targetTracker = new char[8, 8];
char[,] enemyGrid = new char[8, 8];

//infinite loop until player quits the game
while(true)
{
    Main();
}


//creates the main function
void Main()
{
    string choice = DisplayUI();
    if(choice == "1")
    {
        grid = makeEmptyGrid();
        targetTracker = makeEmptyGrid();
        enemyGrid = makeEmptyGrid();

        grid = placeShips(destroyers, submarines, carriers);
        enemyGrid = placeShipRNG();

        while (total != 0 || enemyTotal != 0)
        {
            assembledTurn(playerTurn);
        }
        if(total == 0)
        {
            WriteLine("Computer Wins!");
        }
        else
        {
            WriteLine("Player Wins!");
        }
    }
    else if(choice == "2")
    {

    }
    else if (choice == "3")
    {
        WriteLine("The player and computer each have 5 ships : 2 Destroyers (1 cell), 2 Submarines (2 cells) and 1 Carrier (3 cells) you try to guess\nthe computer's ship locations by typing in your  X and Y coordinates of a cell and check if there is a ship by firing at that cell\nthe result will be shown on your tracker grid and you can adjust your shot accordingly. Remember that the Computer is also shooting at your ships!");
    }
    else if (choice == "4")
    {
        Environment.Exit(0);
    }
}

//Creates the original UI
string DisplayUI()
{
    WriteLine("Enter an option\n1 : Start a new game\n2 : Continue game\n3 : Read Rules\n4 : Quit");
    string choice = ReadLine();
    return choice;
}
//Displays a grid
void DisplayGrids(char[,] grid, char[,] trackerSheet)
{

}

//creates an empty 8x8 grid
char[,] makeEmptyGrid()
{
    char[,] temp = new char[8, 8];
    for (int i = 1; i <= 8; i++)
    {
        for( int b = 1; b <= 8; b++)
        {
            temp[i, b] = ' ';
        }
    }
    return temp;
}

//places the ship
char[,] placeShips(int destroyers, int submarines, int carriers)
{
    int Ycoord = -1;
    int Xcoord = -1;
    bool valid = false;
    int total = 5;
    string shipType = "";
    char[,] tempArray = new char[8, 8];
    while (total > 0)
    { 
        while (!valid)
        {
            WriteLine("Enter ship type\n1 : Destroyer\n2 : Submarine\n3 : Carrier");
            shipType = ReadLine();
            //checks for destroyer amount
            if (shipType == "1")
            {
                if (destroyers != 0)
                {
                    destroyers--;
                    valid = true;
                }
                else
                {
                    WriteLine("You are out of destroyers");
                }
            }
            //checks for submarine amount
            if (shipType == "2")
            {
                if (submarines != 0)
                {
                    submarines--;
                    valid = true;
                }
                else
                {
                    WriteLine("You are out of submarines");
                }
            }
            //checks for carrier amount
            if (shipType == "3")
            {
                if (carriers != 0)
                {
                    carriers--;
                    valid = true;
                }
                else
                {
                    WriteLine("You are out of carriers");
                }
            }
        }
        //asks for ship alignment
        WriteLine("Enter ship alignment\n1 : Vertical\n2 : Horizontal");
        string direction = ReadLine();
        //gets ship starting coordinates
        while (Xcoord > 8 || Xcoord < 1 && ((tempArray[Xcoord, Ycoord] == 'C') || (tempArray[Xcoord, Ycoord] == 'D') || (tempArray[Xcoord, Ycoord] == 'S')) || Xcoord == -1)
        {
            WriteLine("Enter your starting X coordinate: ");
            int temp = Convert.ToInt32(ReadLine());
            if (temp > 0 && temp < 9)
            {
                Xcoord = temp;
            }
            else
            {
                WriteLine("Invalid Input");
            }
        }
        while (Ycoord > 8 || Ycoord < 1 && ((tempArray[Xcoord, Ycoord] == 'C') || (tempArray[Xcoord, Ycoord] == 'D') || (tempArray[Xcoord, Ycoord] == 'S')) || Ycoord == -1)
        {
            WriteLine("Enter your starting Y coordinate: ");
            int temp = Convert.ToInt32(ReadLine());
            if (temp > 0 && temp < 9)
            {
                Ycoord = temp;
            }
            else
            {
                WriteLine("Invalid Input or there is an exitsting ship at this location");
            }
        }
        //places a destroyer
        if (shipType == "1")
        {
            tempArray[Xcoord, Ycoord] = 'D';
        }
        //checks fitting for submarine
        if (shipType == "2")
        {
            if (direction == "1")
            {
                if (Ycoord + 1 > 8)
                {
                    WriteLine("Your ship won't fit");
                }
                else
                {
                    tempArray[Xcoord, Ycoord] = 'S';
                    tempArray[Xcoord, Ycoord + 1] = 'S';
                }
            }
            else
            {
                if (Xcoord + 1 > 8)
                {
                    WriteLine("Your ship won't fit");
                }
                else
                {
                    tempArray[Xcoord, Ycoord] = 'S';
                    tempArray[Xcoord + 1, Ycoord] = 'S';
                }
            }

        }
        //checks fitting for carrier
        if (shipType == "3")
        {
            if (direction == "1")
            {
                if (Ycoord + 2 > 8)
                {
                    WriteLine("Your ship won't fit");
                }
                else
                {
                    tempArray[Xcoord, Ycoord] = 'C';
                    tempArray[Xcoord, Ycoord + 1] = 'C';
                    tempArray[Xcoord, Ycoord + 3] = 'C';
                }
            }
            else
            {
                if (Xcoord + 2 > 8)
                {
                    WriteLine("Your ship won't fit");
                }
                else
                {
                    tempArray[Xcoord, Ycoord] = 'C';
                    tempArray[Xcoord + 1, Ycoord] = 'C';
                    tempArray[Xcoord + 2, Ycoord] = 'C';
                }
            }
        }
        total = destroyers + submarines + carriers;
    }
    return tempArray;
}
//Randomly places ships
char[,] placeShipRNG()
{
    int Ycoord = -1;
    int Xcoord = -1;
    bool valid = false;
    int direction = 0;
    bool tryAgain = true;
    char[,] temp = new char[8, 8];
    Random random = new Random();
    
    //places 2 destroyers
    for (int i = 0; i <= 2; i++)
    {
        while ((temp[Xcoord, Ycoord] == 'C') || (temp[Xcoord, Ycoord] == 'D') || (temp[Xcoord, Ycoord] == 'S') || Xcoord == -1)
        {
            Ycoord = random.Next(1, 9);
            Xcoord = random.Next(1, 9);
            temp[Xcoord, Ycoord] = 'D';
        }
    }
    //places 2 submarines
    for (int i = 0; i <= 2; i++)
    {
        while (tryAgain)
        {
            while ((temp[Xcoord, Ycoord] == 'C') || (temp[Xcoord, Ycoord] == 'D') || (temp[Xcoord, Ycoord] == 'S') || Xcoord == -1)
            {
                direction = random.Next(1, 3);
                Ycoord = random.Next(1, 9);
                Xcoord = random.Next(1, 9);
                if (direction == 1)
                {
                    if (Ycoord + 1 > 8)
                    {
                        tryAgain = true;
                    }
                    else
                    {
                        temp[Xcoord, Ycoord] = 'S';
                        temp[Xcoord, Ycoord + 1] = 'S';
                        tryAgain = false;
                    }
                }
                else
                {
                    if (Xcoord + 1 > 8)
                    {
                        tryAgain = true;
                    }
                    else
                    {
                        temp[Xcoord, Ycoord] = 'S';
                        temp[Xcoord + 1, Ycoord] = 'S';
                        tryAgain = false;
                    }
                }
            }
        }
    }
    //places a carrier
    while (tryAgain)
    {
        while ((temp[Xcoord, Ycoord] == 'C') || (temp[Xcoord, Ycoord] == 'D') || (temp[Xcoord, Ycoord] == 'S') || Xcoord == -1)
        {
            direction = random.Next(1, 3);
            Ycoord = random.Next(1, 9);
            Xcoord = random.Next(1, 9);
            if (direction == 1)
            {
                if (Ycoord + 2 > 8)
                {
                    tryAgain = true;
                }
                else
                {
                    temp[Xcoord, Ycoord] = 'C';
                    temp[Xcoord, Ycoord + 1] = 'C';
                    temp[Xcoord, Ycoord + 2] = 'C';
                    tryAgain = false;
                }
            }
            else
            {
                if (Xcoord + 1 > 8)
                {
                    tryAgain = true;
                }
                else
                {
                    temp[Xcoord, Ycoord] = 'C';
                    temp[Xcoord + 1, Ycoord] = 'C';
                    temp[Xcoord + 2, Ycoord] = 'C';
                    tryAgain = false;
                }
            }
        }
    }
    return temp;

}
//checks for a hit at target
bool fireShot(char[,] grid, int shotX, int shotY)
{
    bool hit = false;
    char target = grid[shotX, shotY];
    if (target == 'C' || target == 'D' || target == 'S')
    {
        hit = true;
    }
    else
    {
        hit = false;
    }
    return hit;
}


//assembles the program for a turn
void assembledTurn(bool playerTurn)
{
    int Xcoord = -1;
    int Ycoord = -1;
    Random rng = new Random();
    if (playerTurn)
    {
        while (Xcoord > 8 || Xcoord < 1)
        {
            WriteLine("Enter the X coordinate of your target cell");
            Xcoord = Convert.ToInt32(ReadLine());
            if (Xcoord > 8 || Xcoord < 1)
            {
                WriteLine("Target out of bounds");
            }
        }
        while (Ycoord > 8 || Ycoord < 1)
        {
            WriteLine("Enter the Y coordinate of your target cell");
            Ycoord = Convert.ToInt32(ReadLine());
            if (Ycoord > 8 || Ycoord < 1)
            {
                WriteLine("Target out of bounds");
            }
        }
        if (fireShot(grid, Xcoord, Ycoord))
        {
            targetTracker[Xcoord, Ycoord] = 'X';
            hitCountPlayer++;
        }
        else
        {
            targetTracker[Xcoord, Ycoord] = '~';
        }
    }
    else
    {
        while (Xcoord > 8 || Xcoord < 1)
        {
            Xcoord = rng.Next(1, 9);
        }
        while (Ycoord > 8 || Ycoord < 1)
        {
            Ycoord = rng.Next(1, 9);
        }
        if (fireShot(grid, Xcoord, Ycoord))
        {
            grid[Xcoord, Ycoord] = 'X';
            hitCountEnemy++;
        }
        else
        {
            grid[Xcoord, Ycoord] = '~';
        }
    }
    playerTurn = !playerTurn;
}
