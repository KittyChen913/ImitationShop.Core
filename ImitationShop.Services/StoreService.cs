namespace ImitationShop.Services;

public class StoreService : IStoreService
{
    private readonly IStoreRepository storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        this.storeRepository = storeRepository;
    }

    public async Task<IEnumerable<Store>> GetStoreItemList(int userId)
    {
        return await storeRepository.GetStoreItemList(userId);
    }

    public async Task<int> AddStoreItem(Store model)
    {
        var result = await storeRepository.Add(model);
        return result.StoreId;
    }

    public async Task<bool> DeleteStoreItem(Store model)
    {
        return await storeRepository.Delete(model.StoreId);
    }
}
