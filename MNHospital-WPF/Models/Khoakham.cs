using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Khoakham
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Bacsi> Bacsis { get; set; } = new List<Bacsi>();
}
