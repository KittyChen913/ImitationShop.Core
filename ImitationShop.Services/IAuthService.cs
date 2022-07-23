namespace ImitationShop.Services;

public interface IAuthService
{
    Task<int> StorageUser(UserRegisterModel model);
    public bool VerifyPassword(string inputPassword, byte[] userPassword);
    string IssueJwtToken(TokenInfoModel model);
}
