namespace ImitationShop.Services;

public interface IItemsService
{
    Task<IEnumerable<Item>> GetItemList();
    Task<Item> GetItem(int itemId);
}
