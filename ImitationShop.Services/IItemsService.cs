namespace ImitationShop.Services;

public interface IItemsService
{
    List<Item> GetItemList();

    Item GetItem(int itemId);
}
