namespace ImitationShop.Services;

public interface IStoreService
{
    Task<IEnumerable<Store>> GetStoreItemList(int userId);
}
