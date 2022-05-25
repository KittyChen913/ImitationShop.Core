namespace ImitationShop.Common.Helpers;

public interface IHashHelper
{
    HashModel GetHash(string password);
    public byte[] ComputeHash(string password, byte[] salt);
}
