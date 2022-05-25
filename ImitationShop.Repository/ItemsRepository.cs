namespace ImitationShop.Repository;

public class ItemsRepository : IRepository<Item>
{
    private readonly ImitationShopDBContext dbContext;

    public ItemsRepository(ImitationShopDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Item>> Query()
    {
        return await dbContext.Items.ToListAsync();
    }

    public async Task<Item> QueryById(object id)
    {
        return await dbContext.Items.FirstAsync(i => i.ItemId == Convert.ToInt32(id));
    }
}
