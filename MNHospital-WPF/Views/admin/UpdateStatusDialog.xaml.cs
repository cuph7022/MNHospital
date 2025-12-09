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
	/// Interaction logic for UpdateStatusDialog.xaml
	/// </summary>
	public partial class UpdateStatusDialog : Window
	{
		private Datlich _item;
		public UpdateStatusDialog(Datlich item)
		{
			_item = item;
			InitializeComponent();
		}

		private void BtnConfirm_Click(object sender, RoutedEventArgs e)
		{
			var selectedStatus = ((ComboBoxItem)cbNewStatus.SelectedItem)?.Tag.ToString();
			int newStatusKey;
			

			
			switch (selectedStatus)
			{
				
				case "Confirmed":
					newStatusKey = (int)OrderStatus.Confirmed;
					break;
				case "Reject":
					newStatusKey = (int)OrderStatus.Reject;
					break;
				default:
					MessageBox.Show("Please select a valid status!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
			}
			    _item.Status = newStatusKey;

			    ManagerAppointment.Instance.UpdateApoiment(_item);
				MessageBox.Show("Update status successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
				DialogResult = true;
			
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
