using System;
using System.Collections.Generic;

namespace ImitationShop.EFCore.DbModels
{
    public partial class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Description { get; set; }
    }
}
