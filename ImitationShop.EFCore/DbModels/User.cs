using System;
using System.Collections.Generic;

namespace ImitationShop.EFCore.DbModels
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string? MailAddress { get; set; }
    }
}
