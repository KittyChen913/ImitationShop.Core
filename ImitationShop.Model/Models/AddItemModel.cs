namespace ImitationShop.Model.Models;

public class AddItemModel
{
    [Required]
    public string ItemName { get; set; }
    [Required]
    public decimal? Price { get; set; }
    [Required]
    public int? Amount { get; set; }
    public string? Description { get; set; }
}
