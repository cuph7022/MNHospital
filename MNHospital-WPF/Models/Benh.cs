using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Benh
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Ketqua> Ketquas { get; set; } = new List<Ketqua>();
}
