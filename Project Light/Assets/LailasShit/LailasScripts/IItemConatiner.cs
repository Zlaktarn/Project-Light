

public interface IItemConatiner
{
    bool ContainItem(Item item);
    int ItemCount(Item item);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();
}


