namespace ImitationShop.Model.Models;

public class BaseRequestModel<T>
{
    [Required]
    public string? RequestId { get; set; }
    public T? Data { get; set; }
}
