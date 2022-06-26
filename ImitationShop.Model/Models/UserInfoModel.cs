namespace ImitationShop.Model.Models;

public class UserInfoModel
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public DateTime CreateDate { get; set; }
    public string? MailAddress { get; set; }
}
