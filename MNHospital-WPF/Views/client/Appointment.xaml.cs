using ManagerLibrary.ModelsViews;
using MNHospital_WPF.Models;
using MNHospital_WPF.ModelsViews;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MNHospital_WPF.Views.client
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Appointment : Window
    {
		private Account _loggedInUser;
		public Appointment(Account loggedInUser)
        {
			_loggedInUser = loggedInUser;
			this.Closing += Profile_Closed;
			InitializeComponent();
			LoadKham();

		}


		private void LoadKham()
		{
			using (var context = new MnHospitalContext())
			{
				var kham = context.Khams.ToList();
				cbKhams.ItemsSource = kham;
			}
		}
		private void BookAppointmentButton_Click(object sender, RoutedEventArgs e)
		{
			if (dpAppointmentDate.SelectedDate == null || cbAppointmentTime.SelectedItem == null)
			{
				MessageBox.Show("Vui lòng chọn ngày và giờ khám.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
		
			DateOnly selectedDate = DateOnly.FromDateTime(dpAppointmentDate.SelectedDate.Value);
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			if (selectedDate < today)
			{
				MessageBox.Show("Ngày khám phải từ hôm nay trở đi.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var appointmentTime = (cbAppointmentTime.SelectedItem as ComboBoxItem).Content.ToString();

			TimeOnly appointmentTimeOnly;
			if (!TimeOnly.TryParse(appointmentTime, out appointmentTimeOnly))
			{
				MessageBox.Show("Không thể chuyển đổi giờ khám. Vui lòng chọn lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			using (var context = new MnHospitalContext())
			{
				var benhnhan = context.Benhnhans
					.FirstOrDefault(b => b.Username == _loggedInUser.Username);

				if (benhnhan == null)
				{
					MessageBox.Show("Không tìm thấy thông tin bệnh nhân.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				var existingAppointment = context.Datliches
		   .FirstOrDefault(d => d.Ngaykham == selectedDate && d.Giokham == appointmentTimeOnly );

				if (existingAppointment != null)
				{
					MessageBox.Show("Ca khám này đã được đặt vào thời gian và ngày này. Vui lòng chọn thời gian khác.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}


				var appointment = new Datlich
				{
					Ngaykham = selectedDate,
					Giokham = appointmentTimeOnly,
					Idbenhnhan = benhnhan.Id,
					Idkham = (int)cbKhams.SelectedValue,
					Status = 0
				};

				ManagerAppointment.Instance.AddApoiment(appointment);
			}

			MessageBox.Show("Đặt lịch thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

			this.Close();
		}


		private void Profile_Closed(object sender, EventArgs e)
		{

			Home home = new Home(_loggedInUser);
			home.Show();
		}
	}
}
