namespace ImitationShop.Services;

public class ItemsService : IItemsService
{
    private readonly IBaseRepository<Item> itemRepository;

    public ItemsService(IBaseRepository<Item> itemRepository)
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
