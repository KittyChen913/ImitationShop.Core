namespace ImitationShop.Services;

public interface IUserService
{
    Task<User> GetUserByName(string userName);
    Task<int> AddUser(User model);
}
