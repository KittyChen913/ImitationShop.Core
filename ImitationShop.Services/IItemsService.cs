namespace ImitationShop.Services;

public interface IItemsService
{
    Task<List<Item>> GetItemList();
    Task<Item> GetItem(int itemId);
}
