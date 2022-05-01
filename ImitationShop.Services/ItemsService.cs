using ImitationShop.EFCore.DbModels;
using ImitationShop.Repository;

namespace ImitationShop.Services;

public class ItemsService : IItemsService
{
    private readonly ItemsRepository itemRepository;

    public ItemsService(ItemsRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public List<Item> GetItemList()
    {
        return itemRepository.Query();
    }
}
