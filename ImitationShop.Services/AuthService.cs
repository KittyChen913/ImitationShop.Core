namespace ImitationShop.Services;

public class AuthService : IAuthService
{
    private readonly JwtTokenModel _jwtSettings;
    private readonly IHashHelper hashHelper;
    private readonly IUserService userService;

    public AuthService(IHashHelper hashHelper, IUserService userService, IOptions<JwtTokenModel> options)
    {
        _jwtSettings = options.Value;
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

    public string IssueJwtToken(TokenInfoModel model)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, !string.IsNullOrWhiteSpace(model.UserName)? model.UserName : string.Empty),
            new (JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer!),
            new (JwtRegisteredClaimNames.Exp, DateTime.Now.AddHours(_jwtSettings.Expires).ToString(CultureInfo.InvariantCulture)),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var jwtKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret!));
        var signingCredentials = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(_jwtSettings.Expires),
            SigningCredentials = signingCredentials
        };

        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.CreateToken(tokenDescriptor);
        var serializeJwtToken = jwtHandler.WriteToken(jwtToken);

        return serializeJwtToken;
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
