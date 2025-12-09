using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Ketqua
{
    public int Id { get; set; }

    public int? Idbenh { get; set; }

    public int? Idbenhnhan { get; set; }

    public int? Idbacsi { get; set; }

    public DateOnly? Ngaykham { get; set; }

    public string? Chuandoan { get; set; }

    public string? Ketqua1 { get; set; }

    public virtual ICollection<Hoso> Hosos { get; set; } = new List<Hoso>();

    public virtual Bacsi? IdbacsiNavigation { get; set; }

    public virtual Benh? IdbenhNavigation { get; set; }

    public virtual Benhnhan? IdbenhnhanNavigation { get; set; }
}
