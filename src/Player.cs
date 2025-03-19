using System.Runtime.CompilerServices;

class Player
{
    private int health;
    private int maxHealth = 100;

    public Player()
    {
        health = maxHealth;
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

    public bool TakeFromChest(string itemName)
    {
        // TODO implement:
        // // Remove the Item from the Room
        // // Put it in your backpack.
        // // Inspect returned values.
        // // If the item doesn't fit your backpack, put it back in the chest.
        // // Communicate to the user what's happening.
        // // Return true/false for success/failure
        return false;
    }
}
