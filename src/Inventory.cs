using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    // methods
    public int TotalWeight()
    {
        int total = 30;

        // Loop though the items and add all the weights
        Item medkit = new(25, "medkit");
        Item bat = new(10, "bat");
        Item knife = new(5, "knife");
        Item katana = new(15, "katana");


        return total;
    }

    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    public bool Put(string itemName, Item item)
    {
        if (item.Weight > FreeWeight())
        {
            return false;
        }

        items.Add(itemName, item);
        return true;
    }

    public Item Get(string itemName)
    {
        if(items.ContainsKey(itemName)){
            Item item = items[itemName];
            items.Remove(itemName);
            return item;
        }
        return null;
    }

}