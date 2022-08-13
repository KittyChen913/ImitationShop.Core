namespace ImitationShop.Repository;

public class StoreRepository : BaseRepository<Store>, IStoreRepository
{
    private readonly ImitationShopDBContext dbContext;

    public StoreRepository(ImitationShopDBContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Store>> GetStoreItemList(int userId)
    {
        return await dbContext.Stores.Where(store => store.UserId == userId).ToListAsync();
    }
}
