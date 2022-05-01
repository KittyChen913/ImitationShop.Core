namespace ImitationShop.EFCore.DbModels;

public partial class Item
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = null!;
    public decimal? ItemPrice { get; set; }
}
