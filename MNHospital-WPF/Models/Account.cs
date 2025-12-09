using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string? Userpassword { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<Bacsi> Bacsis { get; set; } = new List<Bacsi>();

    public virtual ICollection<Benhnhan> Benhnhans { get; set; } = new List<Benhnhan>();

    public virtual Role? RoleNavigation { get; set; }
}
