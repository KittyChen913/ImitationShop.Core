using System;
using System.Collections.Generic;

namespace ImitationShop.EFCore.DbModels
{
    public partial class Store
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
    }
}
