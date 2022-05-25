namespace ImitationShop.Common.Helpers;

public class HashHelper : IHashHelper
{
    private readonly int Iterations = 50000;

    public HashModel GetHash(string password)
    {
        byte[] salt = GenerateSalt();
        byte[] hashed = ComputeHash(password, salt);
        return new HashModel { Salt = salt, Hashed = hashed };
    }

    public byte[] ComputeHash(string password, byte[] salt)
    {
        var rfc289 = new Rfc2898DeriveBytes(password, salt, Iterations);
        var hashed = rfc289.GetBytes(32);
        Console.WriteLine($"hash: {Convert.ToBase64String(hashed)}");
        return hashed;
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rngCsp = RandomNumberGenerator.Create())
        {
            rngCsp.GetNonZeroBytes(salt);
            Console.WriteLine($"salt: {Convert.ToBase64String(salt)}");
        }
        return salt;
    }
}
