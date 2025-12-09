using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Hoso
{
    public int Id { get; set; }

    public int? Idbenhnhan { get; set; }

    public int? Idketqua { get; set; }

    public virtual Benhnhan? IdbenhnhanNavigation { get; set; }

    public virtual Ketqua? IdketquaNavigation { get; set; }
}
