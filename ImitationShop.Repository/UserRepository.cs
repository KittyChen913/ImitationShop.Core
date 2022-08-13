namespace ImitationShop.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ImitationShopDBContext dbContext;

    public UserRepository(ImitationShopDBContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<User> QueryByUserName(string userName) => await dbContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(userName));
}
