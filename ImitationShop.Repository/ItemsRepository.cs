namespace ImitationShop.Repository;

public class ItemsRepository : IRepository<Item>
{
    private readonly ImitationShopDBContext dbContext;

    public ItemsRepository(ImitationShopDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Item> Query()
    {
        return dbContext.Items.ToList();
    }

    public Item QueryById(object id)
    {
        return dbContext.Items.First(i => i.ItemId == Convert.ToInt32(id));
    }
}
