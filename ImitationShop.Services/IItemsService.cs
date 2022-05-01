using ImitationShop.EFCore.DbModels;

namespace ImitationShop.Services
{
    public interface IItemsService
    {
        List<Item> GetItemList();
    }
}