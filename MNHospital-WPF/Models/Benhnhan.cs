using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Benhnhan
{
    public int Id { get; set; }

    public string? Cccd { get; set; }

    public string? Username { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Address { get; set; }

    public string? Baohiem { get; set; }

    public virtual ICollection<Datlich> Datliches { get; set; } = new List<Datlich>();

    public virtual ICollection<Hoso> Hosos { get; set; } = new List<Hoso>();

    public virtual ICollection<Ketqua> Ketquas { get; set; } = new List<Ketqua>();

    public virtual Account? UsernameNavigation { get; set; }
}
