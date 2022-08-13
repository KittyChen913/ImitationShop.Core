namespace ImitationShop.Repository;

public class UserRepository : IRepository<User>
{
    private readonly ImitationShopDBContext dBContext;

    public UserRepository(ImitationShopDBContext dBContext)
    {
        this.dBContext = dBContext;
    }

    public async Task<int> Add(User model)
    {
        await dBContext.Users.AddAsync(model);
        dBContext.SaveChanges();

        return model.UserId;
    }

    public Task<IEnumerable<User>> Query()
    {
        throw new NotImplementedException();
    }

    public Task<User> QueryById(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> QueryByString(string userName)
    {
        return await dBContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(userName));
    }
}
