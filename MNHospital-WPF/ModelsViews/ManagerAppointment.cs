using ManagerLibrary.ModelsViews;
using MNHospital_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNHospital_WPF.ModelsViews
{
  internal class ManagerAppointment
	{
		private static ManagerAppointment _instance = null;
		private static readonly object _instanceLock = new object();

		private ManagerAppointment() { }

		public static ManagerAppointment Instance
		{
			get
			{
				lock (_instanceLock)
				{
					if (_instance == null)
					{
						_instance = new ManagerAppointment();
					}
					return _instance;
				}
			}
		}

		public void AddApoiment(Datlich s)
		{
			lock (_instanceLock)
			{
				try
				{
					using (var context = new MnHospitalContext())
					{
						context.Datliches.Add(s);
						context.SaveChanges();
					}
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
			}
		}

		public void UpdateApoiment(Datlich s)
		{
			lock (_instanceLock)
			{
				try
				{
					using (var context = new MnHospitalContext())
					{
						context.Datliches.Update(s);
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
