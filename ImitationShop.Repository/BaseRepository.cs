namespace ImitationShop.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ImitationShopDBContext dbContext;
    private readonly DbSet<TEntity> entities;

    public BaseRepository(ImitationShopDBContext dbContext)
    {
        this.dbContext = dbContext;
        entities = dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Query() => await entities.ToListAsync();

    public async Task<TEntity> QueryById(object primaryKeyId) => await entities.FindAsync(primaryKeyId);

    public async Task<TEntity> Add(TEntity model)
    {
        await entities.AddAsync(model);
        dbContext.SaveChanges();
        return model;
    }
}
