namespace ImitationShop.Services;

public interface IAuthService
{
    Task<int> StorageUser(UserRegisterModel model);
}
