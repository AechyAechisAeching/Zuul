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
        Room door = new Room("You found a exit??..");

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
        office.AddExit("north", door);
        office.AddExit("south", secondHallway);
        secondHallway.AddExit("west", livingroom);
        livingroom.AddExit("east", secondHallway);

    

        // Add items to rooms
        Item medkit = new Item(15, "medkit");
        Item bat = new Item(10, "bat");
        Item knife = new Item(5, "knife");
        Item katana = new Item(15, "katana");

        // Add items to the room
        bedroom.AddItem("medkit", medkit);
        kitchen.AddItem("knife", knife);
        office.AddItem("katana", katana);
        bathroom.AddItem("bat", bat);

        // Set player's starting position
        player.CurrentRoom = bedroom;
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


        // Give explanation to the game
    private void PrintWelcome()
    {
        //Intro
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
                case "take":
                    Take(command);
                    break;
                case "drop":
                    Drop(command);
                    break;
                case "use":
                    Use(command);
                    break;
            }
        }
        else
        {
            // If the health is 0.. the player can no longer send any commands except quit.
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

        // Now explicitly show the items in the room
        if (player.CurrentRoom.Chest.Items.Count > 0)
        {
            Console.WriteLine("Items in the room:");
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

    // Give a starting dialogue and help commands
    private void PrintHelp()
    {
        Console.WriteLine("You just woke up from a wonderful dream.");
        Console.WriteLine("You decided to stand up and wander around your house.");
        Console.WriteLine("Your goal is to escape the mansion, avoid having any contact with anyone!");
        Console.WriteLine();
        // Prints all valid commands from COmmandlibrary
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
        player.PrintInventory();
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

        player.Damage(10);

        if (player.IsAlive()) 
        {
            Console.WriteLine(player.CurrentRoom.GetLongDescription());
        }
        else
        {
            Console.WriteLine("You are dead.");
        }
    }



        // Has the player taken the item?
    private void Take(Command command)
    {
        if (!command.HasSecondWord())
        {
            // The Item is not defined and you have to clarify what   item
            Console.WriteLine("Take what?");
            return;
        }
        // Player gave clarified information what Item it is
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

    // New Use method to handle the use command
    private void Use(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Use what?");
            return;
        }

        string itemName = command.SecondWord;
        bool success = player.Use(itemName);  // Use the item in the player's backpack
        if (!success)
        {
            Console.WriteLine("You cannot use that item.");
        }
    }
}
