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

    public async Task<bool> Update(TEntity model)
    {
        var primaryKeyId = GetPrimaryKeyValue(model);

        var entity = await entities.FindAsync(primaryKeyId);
        if (entity != null)
        {
            dbContext.Entry(entity).CurrentValues.SetValues(model);
        }
        return (await dbContext.SaveChangesAsync()) > 0;
    }

    private int GetPrimaryKeyValue(TEntity entity)
    {
        var keyName = dbContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Single().Name;
        return (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);
    }
}
