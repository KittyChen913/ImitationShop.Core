namespace ImitationShop.Model.Models;

public class JwtTokenModel
{
    public string? Secret { get; set; }
    public string? Issuer { get; set; }
    public int Expires { get; set; }
}
