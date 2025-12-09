using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Kham
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public TimeOnly? Time { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<Datlich> Datliches { get; set; } = new List<Datlich>();
}
