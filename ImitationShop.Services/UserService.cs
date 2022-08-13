namespace ImitationShop.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<int> AddUser(User model)
    {
        var userInfo = await userRepository.Add(model);
        return userInfo.UserId;
    }

    public async Task<User> GetUserByName(string userName)
    {
        return await userRepository.QueryByUserName(userName);
    }
}
