namespace ImitationShop.Repository;

public class ItemsRepository : BaseRepository<Item>
{
    private readonly ImitationShopDBContext dbContext;

    public ItemsRepository(ImitationShopDBContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
}
