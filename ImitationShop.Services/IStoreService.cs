namespace ImitationShop.Services;

public interface IStoreService
{
    Task<IEnumerable<Store>> GetStoreItemList(int userId);
    Task<int> AddStoreItem(Store model);
    Task<bool> DeleteStoreItem(Store model);
}
