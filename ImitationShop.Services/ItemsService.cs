namespace ImitationShop.Services;

public class ItemsService : IItemsService
{
    private readonly IRepository<Item> itemRepository;

    public ItemsService(IRepository<Item> itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public async Task<IEnumerable<Item>> GetItemList()
    {
        return await itemRepository.Query();
    }

    public async Task<Item> GetItem(int itemId)
    {
        return await itemRepository.QueryById(itemId);
    }
}
