using System;

class Game
{
	// Private fields
	private Parser parser;
	private Room currentRoom;

	// Constructor
	public Game()
	{
		parser = new Parser();
		CreateRooms();
	}

	class Player {
    public Room CurrentRoom { get; set; }

    public Player() {
        CurrentRoom = null;
    }
	
	private void Take(Command command)
	{
		// Implement
	}
	private void Drop(Command command) {
		// Implement
	}
}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room bedroom = new Room("You are now in your bedroom.");
		Room hallway = new Room("You are now in the hallway.");
		Room bathroom = new Room("You entered the bathroom.");
		Room kitchen = new Room("You are now in the kitchen, something smells good.");
		Room staircase = new Room("You are walking down the stairs.");
		Room office = new Room("You entered your office.");
		Room secondHallway = new Room("You are back in a hallway.");
		Room livingroom = new Room("You entered the living room.");

		// Initialise room exits

		bedroom.AddExit("north", hallway);
		hallway.AddExit("south", bedroom);

		hallway.AddExit("east", bathroom);
		bathroom.AddExit("west", hallway);

		hallway.AddExit("north", staircase);
		staircase.AddExit("south", hallway);

		// Downstairs

		staircase.AddExit("north", secondHallway);
		secondHallway.AddExit("south", staircase);

		secondHallway.AddExit("east", kitchen);
		kitchen.AddExit("west", secondHallway);

		secondHallway.AddExit("north", office);
		office.AddExit("south", secondHallway);

		secondHallway.AddExit("west", livingroom);
		livingroom.AddExit("east", secondHallway);

		// Create your Items here
		Item medkit = new(25, "medkit");
		Item bat = new(10, "bat");
		Item knife = new(5, "knife");
		Item katana = new(15, "katana");

		// And add them to the Rooms
		// ...

		// Game starts in the bedroom, you wake up.
		currentRoom = bedroom;
	}



		
	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.~
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

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(currentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
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
			// case "look";
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You just woke up from a wonderful dream.");
		Console.WriteLine("You decided to stand up and wander around your house.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}


		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = currentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		currentRoom = nextRoom;
		Console.WriteLine(currentRoom.GetLongDescription());
	}
}
