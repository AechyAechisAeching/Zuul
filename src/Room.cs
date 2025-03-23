class Room
{
    private string description;
    private Dictionary<string, Room> exits;
    private Inventory chest;  // This will hold the items in the room

    // Constructor: Initializes the room's description, exits, and chest
    public Room(string desc)
    {
        description = desc;
        exits = new Dictionary<string, Room>();
        chest = new Inventory(999999);  // A large max weight for the room's chest
    }

    // Adds an exit from this room to another room in a specified direction
    public void AddExit(string direction, Room neighbor)
    {
        exits.Add(direction, neighbor);
    }

    // Returns the short description of the room
    public string GetShortDescription()
    {
        return description;
    }

    // Returns a long description of the room including exits and items in the room
  public string GetLongDescription()
{
    string str = description + ".\n";
    str += GetExitString();
    return str;
}

    // Returns the neighboring room if an exit in the specified direction exists
    public Room GetExit(string direction)
    {
        if (exits.ContainsKey(direction))
        {
            return exits[direction];
        }
        return null;
    }

    // Returns a string representing all exits of the current room
    private string GetExitString()
    {
        string str = "Exits: ";
        str += String.Join(", ", exits.Keys);
        return str;
    }

    // Adds an item to the chest in the room
    public void AddItem(string itemName, Item item)
    {
        chest.Put(itemName, item);
    }

    // Removes an item from the chest in the room
    public bool RemoveItem(string itemName)
    {
        Item item = chest.Get(itemName);
        if (item != null)
        {
            // Item has been removed from the chest
            return true;
        }
        return false;
    }

    // Returns a dictionary of all items in the room's inventory
    public Dictionary<string, Item> GetItems()
    {
        return chest.Items;
    }

    // Property for the chest (inventory)
    public Inventory Chest
    {
        get { return chest; }
    }
}
