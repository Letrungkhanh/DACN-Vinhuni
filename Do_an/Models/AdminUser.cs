using System;
using System.Collections.Generic;

namespace Do_an.Models;

public partial class AdminUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }
}
