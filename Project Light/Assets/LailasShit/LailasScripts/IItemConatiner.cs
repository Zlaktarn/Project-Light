

public interface IItemConatiner
{
    int ItemCount(string itemID);
    Item RemoveItem(string itemID);
    bool RemoveItem(Item item); 
    bool AddItem(Item item, int amount = 1);
    bool IsFull();
    bool AddItem(Item item);

    void Clear();
}


