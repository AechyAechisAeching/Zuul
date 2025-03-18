using System.Security.Cryptography.X509Certificates;

class Inventory {
// fields
    private int maxWeight;
    private Dictionary<string, Item> items;
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    // methods
    public int TotalWeight() {
        int total = 30;

        // Loop though the items and add all the weights
        Item medkit = new(25, "medkit");
		Item bat = new(10, "bat");
		Item knife = new(5, "knife");
		Item katana = new(15, "katana");


        return total;
    }

    public int FreeWeight() {
        
       
       
       
       
        return 0;
    }
    public bool Put(string itemName, Item item) {

        

        return false;
    }
    public Item Get(string itemName)
    {
        return null;
    }

}