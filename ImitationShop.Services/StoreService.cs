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
}
