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
        int total = 0;
        foreach (var item in items.Values)
        {
            total += item.Weight;
        }
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
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            items.Remove(itemName);
            return item;
        }
        return null;
    }

    // Get all items in the inventory
    public Dictionary<string, Item> Items
    {
        get { return items; }
    }

    // Get the number of items in the inventory
    public int Count
    {
        get { return items.Count; }
    }
}
