using System;
using System.Collections.Generic;

class Game
{
    private Parser parser;
    private Player player;
    public Game()
    {
        parser = new Parser();
        player = new Player();
        CreateRooms();
    }

    private void CreateRooms()
    {
        Room bedroom = new Room("You are now in your bedroom.");
        Room hallway = new Room("You are now in the hallway.");
        Room bathroom = new Room("You entered the bathroom.");
        Room kitchen = new Room("You are now in the kitchen, something smells good.");
        Room staircase = new Room("You are walking down the stairs.");
        Room office = new Room("You entered your office.");
        Room secondHallway = new Room("You are back in a hallway.");
        Room livingroom = new Room("You entered the living room.");

        // Set up room exits
        bedroom.AddExit("north", hallway);
        hallway.AddExit("south", bedroom);

        hallway.AddExit("east", bathroom);
        bathroom.AddExit("west", hallway);

        hallway.AddExit("north", staircase);
        staircase.AddExit("south", hallway);

        staircase.AddExit("north", secondHallway);
        secondHallway.AddExit("south", staircase);

        secondHallway.AddExit("east", kitchen);
        kitchen.AddExit("west", secondHallway);

        secondHallway.AddExit("north", office);
        office.AddExit("south", secondHallway);

        secondHallway.AddExit("west", livingroom);
        livingroom.AddExit("east", secondHallway);

        // Add items to rooms
        Item medkit = new(25, "medkit");
        Item bat = new(10, "bat");
        Item knife = new(5, "knife");
        Item katana = new(15, "katana");

       {
    // Add items to the room
    bedroom.AddItem("medkit", medkit);
    kitchen.AddItem("knife", knife);

    // Set player's starting position
    player.CurrentRoom = bedroom;
}
    }

    public void Play()
    {
        PrintWelcome();

        bool finished = false;
        while (!finished)
        {
            Command command = parser.GetCommand();
            finished = ProcessCommand(command);
        }

        Console.WriteLine("Thank you for playing.");
        Console.WriteLine("Press [Enter] to continue.");
        Console.ReadLine();
    }

    private void PrintWelcome()
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Zuul!");
        Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
        Console.WriteLine("Type 'help' if you need help.");
        Console.WriteLine();
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    }

    private bool ProcessCommand(Command command)
   {
    bool wantToQuit = false;

    if (player.IsAlive())
    {
        if (command.IsUnknown())
        {
            Console.WriteLine("I don't know what you mean...");
            return wantToQuit;
        }

        switch (command.CommandWord)
        {
            case "help":
                PrintHelp();
                break;
            case "go":
                GoRoom(command);
                break;
            case "quit":
                wantToQuit = true;
                break;
            case "status":
                Status();
                break;
            case "look":
                Look();
                break;
        }
    }
    else
    {
        // If the healthbar is 0..
		//  the player can no longer sent any commands and can only use the command quit.
        if (command.CommandWord == "quit")
        {
            wantToQuit = true;
        }
        else
        {
            Console.WriteLine("Welcome to the afterlife, if I were you I'd quit.");
        }
    }

    return wantToQuit;
}
	public void Look()
{

    // Describe the current room
    Console.WriteLine(player.CurrentRoom.GetLongDescription());

    // Check if the chest is available in the current room
    if (player.CurrentRoom.Chest != null && player.CurrentRoom.Chest.Count > 0)
    {
        Console.WriteLine("Items in the chest:");
        foreach (var item in player.CurrentRoom.Chest.Items)
        {
            Console.WriteLine($"- {item.Key} ({item.Value.Weight} KG)");
        }
    }
    else
    {
        Console.WriteLine("There are no items in the room.");
    }
}

    private void PrintHelp()
    {
        Console.WriteLine("You just woke up from a wonderful dream.");
        Console.WriteLine("You decided to stand up and wander around your house.");
        Console.WriteLine();
        parser.PrintValidCommands();
    }
	
   private void Status()
{
    if (!player.IsAlive())
    {
        Console.WriteLine("You are dead.");
        return;  // If the player has died, no commands can be used.
    }

    Console.WriteLine("Your health = " + player.GetHealth());
}


    private void GoRoom(Command command)
{
    if (!command.HasSecondWord())
    {
        Console.WriteLine("Go where?");
        return;
    }

    string direction = command.SecondWord;

    // Try to go to the next room.
    Room nextRoom = player.CurrentRoom.GetExit(direction);
    if (nextRoom == null)
    {
        Console.WriteLine("There is no door to " + direction + "!");
        return;
    }

    // Change the current room to the next room
    player.CurrentRoom = nextRoom;

    
    player.Damage(5);

    if (player.IsAlive()) 
    {
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    }
    else
    {
        Console.WriteLine("You are dead.");
    }
}

    private void Take(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Take what?");
            return;
        }

        string itemName = command.SecondWord;
        player.TakeFromChest(itemName);
    }

    private void Drop(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Drop what?");
            return;
        }

        string itemName = command.SecondWord;
        player.DropToChest(itemName);
    }
}
