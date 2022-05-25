namespace ImitationShop.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepository;

    public UserService(IRepository<User> userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<int> AddUser(User model)
    {
        var userId = await userRepository.Add(model);
        return userId;
    }

    public async Task<User> GetUserByName(string userName)
    {
        return await userRepository.QueryByString(userName);
    }
}
