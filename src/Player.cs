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
}
