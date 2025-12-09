using MNHospital_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNHospital_WPF.ModelsViews
{
	internal class ManagerResult
	{
		private static ManagerResult _instance = null;
		private static readonly object _instanceLock = new object();

		private ManagerResult() { }

		public static ManagerResult Instance
		{
			get
			{
				lock (_instanceLock)
				{
					if (_instance == null)
					{
						_instance = new ManagerResult();
					}
					return _instance;
				}
			}
		}

		public void AddResult(Ketqua s)
		{
			lock (_instanceLock)
			{
				try
				{
					using (var context = new MnHospitalContext())
					{
						context.Ketquas.Add(s);
						context.SaveChanges();
					}
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
			}
		}
	}
}



