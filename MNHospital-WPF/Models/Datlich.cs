using MNHospital_WPF.ModelsViews;
using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models;

public partial class Datlich
{
	public int Id { get; set; }

    public DateOnly Ngaykham { get; set; }

    public TimeOnly Giokham { get; set; }

    public int? Idbenhnhan { get; set; }

    public int? Idkham { get; set; }

	public int Status { get; set; }

	public virtual Benhnhan? IdbenhnhanNavigation { get; set; }

    public virtual Kham? IdkhamNavigation { get; set; }

	public string StatusDisplay => Status switch
	{
		(int)OrderStatus.Confirmed => "Đã xác nhận",
		(int)OrderStatus.Reject => "Từ chối"

	};
}
