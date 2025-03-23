class Player
{
    private Inventory backpack;
    public Room CurrentRoom;
    private int health;
    private int maxHealth = 100;

    // Constructor
    public Player()
    {
        health = 100;
        backpack = new Inventory(30);
    }

    public bool TakeFromChest(string itemName)
    {
        Item pItem = CurrentRoom.Chest.Get(itemName);
        if (pItem != null && backpack.FreeWeight() - pItem.Weight >= 0)
        {
            backpack.Put(itemName, pItem);
            Console.WriteLine($"{itemName} has been added to your backpack.");
            return true;
        }
        else
        {
            Console.WriteLine("There's no space left in your backpack.");
            return false;
        }
    }

    public bool DropToChest(string itemName)
    {
        Item pItem = backpack.Get(itemName);
        if (pItem == null)
        {
            Console.WriteLine("You don't have that item in your backpack.");
            return false;
        }
        CurrentRoom.Chest.Put(itemName, pItem);
        Console.WriteLine($"{itemName} has been dropped into the chest.");
        return true;
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public int GetHealth()
    {
        return health;
    }

    // New method to print the player's backpack inventory
    public void PrintInventory()
    {
        if (backpack.Count == 0)
        {
            Console.WriteLine("Your backpack is empty.");
        }
        else
        {
            Console.WriteLine("You are carrying the following items:");
            foreach (var item in backpack.Items)
            {
                Console.WriteLine($"- {item.Key} ({item.Value.Weight} KG)");
            }
        }
    }
}
