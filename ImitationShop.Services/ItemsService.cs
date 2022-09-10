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

    public async Task<int> AddItem(AddItemModel model)
    {
        var result = await itemRepository.Add(new Item
        {
            ItemName = model.ItemName,
            Price = (decimal)model.Price,
            Amount = (int)model.Amount,
            CreateDate = DateTime.Now,
            Description = model.Description
        });
        return result.ItemId;
    }

    public async Task<bool> UpdateItem(Item model)
    {
        return await itemRepository.Update(model);
    }

    public async Task<bool> DeleteItem(int itemId)
    {
        return await itemRepository.Delete(itemId);
    }
}
