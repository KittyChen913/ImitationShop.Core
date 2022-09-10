namespace ImitationShop.Services;

public interface IItemsService
{
    Task<IEnumerable<Item>> GetItemList();
    Task<Item> GetItem(int itemId);
    Task<int> AddItem(AddItemModel model);
    Task<bool> UpdateItem(Item model);
    Task<bool> DeleteItem(int itemId);
}
