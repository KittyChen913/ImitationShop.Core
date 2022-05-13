namespace ImitationShop.Services;

public class ItemsService : IItemsService
{
    private readonly IRepository<Item> itemRepository;

    public ItemsService(IRepository<Item> itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public List<Item> GetItemList()
    {
        return itemRepository.Query();
    }

    public Item GetItem(int itemId)
    {
        return itemRepository.QueryById(itemId);
    }

}
