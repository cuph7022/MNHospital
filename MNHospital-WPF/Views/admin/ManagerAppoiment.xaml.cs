using MNHospital_WPF.Models;
using MNHospital_WPF.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MNHospital_WPF.Views.admin
{
	/// <summary>
	/// Interaction logic for ManagerApoiment.xaml
	/// </summary>
	public partial class ManagerAppoiment : Window
	{
		private Account _loggedInUser;
		public ManagerAppoiment(  Account loggedInUser)
		{
			_loggedInUser = loggedInUser;
			this.Closing += Profile_Closed;
			InitializeComponent();
			LoadAppointments();
		}

		private void LoadAppointments()
		{
		
			using (var context = new MnHospitalContext()) 
			{
				var appointments = from d in context.Datliches
								   join b in context.Benhnhans on d.Idbenhnhan equals b.Id
								   join k in context.Khams on d.Idkham equals k.Id 
								
								   select new 
								   {   KhamId = d.Idkham,
									   BenhnhanName = b.Name,
									   Ngaykham = d.Ngaykham,
									   Giokham = d.Giokham,
									   StatusDisplay = d.StatusDisplay,
									   ServiceName = k.Name,
									   Enable =  d.Status == (int)OrderStatus.Reject ? false : true,
								   };

			
				AppointmentDataGrid.ItemsSource = appointments.ToList();
			}
		}

		private void UpdateStatus_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button == null) return;
			var KhamId = (int)button.Tag;

			using (var context = new MnHospitalContext())
			{
				var app = (from bd in context.Datliches
							  where bd.Idkham == KhamId
							  select bd).FirstOrDefault();

				if (app != null)
				{
					UpdateStatusDialog dialog = new UpdateStatusDialog(app);
					if (dialog.ShowDialog() == true)
					{
						LoadAppointments();
					}
				}
				else
				{
					MessageBox.Show("No borrow details found for the selected item.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
		}

		private void Profile_Closed(object sender, EventArgs e)
		{
			Home home = new Home(_loggedInUser);
			home.Show();
		}

	}
}

