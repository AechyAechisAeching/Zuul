class Room
{
    private string description;
    private Dictionary<string, Room> exits;
    private Inventory chest;  // This will hold the items in the room

    public Room(string desc)
    {
        description = desc;
        exits = new Dictionary<string, Room>();
        chest = new Inventory(999999);  // A large max weight for the room's chest
    }

    public void AddExit(string direction, Room neighbor)
    {
        exits.Add(direction, neighbor);
    }

    public string GetShortDescription()
    {
        return description;
    }

    public string GetLongDescription()
    {
        string str = description + ".\n";
        str += GetExitString();
        return str;
    }

    public Room GetExit(string direction)
    {
        if (exits.ContainsKey(direction))
        {
            return exits[direction];
        }
        return null;
    }

    private string GetExitString()
    {
        string str = "Exits: ";
        str += String.Join(", ", exits.Keys);
        return str;
    }

    public void AddItem(string itemName, Item item)
    {
        chest.Put(itemName, item);
    }

    public void RemoveItem(string itemName)
    {
        chest.Get(itemName);
    }

    // Get all items in the room's inventory
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
