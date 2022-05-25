namespace ImitationShop.Services;

public class AuthService : IAuthService
{
    private readonly IHashHelper hashHelper;
    private readonly IUserService userService;

    public AuthService(IHashHelper hashHelper, IUserService userService)
    {
        this.hashHelper = hashHelper;
        this.userService = userService;
    }

    public async Task<int> StorageUser(UserRegisterModel model)
    {
        var hashResult = hashHelper.GetHash(model.Password!);
        var password = CombinedPassword(hashResult.Hashed!, hashResult.Salt!);

        var userId = await userService.AddUser(new User
        {
            UserName = model.UserName!,
            Password = password,
            CreateDate = DateTime.Now,
            MailAddress = model.MailAddress,
        });

        return userId;
    }

    public bool VerifyPassword(string inputPassword, byte[] userPassword)
    {
        var salt = SplitPasswordGetSalt(userPassword);
        var hash = SplitPasswordGetHash(userPassword);
        var inputHashResult = hashHelper.ComputeHash(inputPassword, salt);

        return inputHashResult.SequenceEqual(hash);
    }

    private byte[] CombinedPassword(byte[] hash, byte[] salt)
    {
        var password = new byte[hash.Length + salt.Length];

        Buffer.BlockCopy(salt, 0, password, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, password, salt.Length, hash.Length);

        return password;
    }

    private byte[] SplitPasswordGetSalt(byte[] password)
    {
        return password.Take(16).ToArray();
    }

    private byte[] SplitPasswordGetHash(byte[] password)
    {
        return password.Skip(16).Take(32).ToArray();
    }
}
