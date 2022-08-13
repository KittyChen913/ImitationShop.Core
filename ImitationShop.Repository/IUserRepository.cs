namespace ImitationShop.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> QueryByUserName(string userName);
}
