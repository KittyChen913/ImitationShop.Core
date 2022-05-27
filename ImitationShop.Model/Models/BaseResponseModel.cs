namespace ImitationShop.Model.Models;

public class BaseResponseModel<T>
{
    public string? RequestId { get; set; } = Guid.NewGuid().ToString();
    public string? ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T? Data { get; set; }
}
