using Microsoft.VisualBasic.ApplicationServices;
using MNHospital_WPF.Models;
using MNHospital_WPF.Views.admin;
using MNHospital_WPF.Views.client;
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

namespace MNHospital_WPF.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
		private Account _loggedInUser;
		public Home(Account loggedInUser)
        {
			_loggedInUser = loggedInUser;
			InitializeComponent();
			UpdateLoginState();

		}

		private void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			Login login = new Login();
			login.Show();
			this.Hide();
		}
		private void UpdateLoginState()
		{
			if (_loggedInUser != null)
			{
				txtUserName.Visibility = Visibility.Visible;
				btnLogout.Visibility = Visibility.Visible;
				btnLogin.Visibility = Visibility.Collapsed;
                

                if (_loggedInUser.Role == 1) // Admin
				{
					txtUserName.Text = $"Xin chào Admin: {_loggedInUser.Username}";
					btnManageAppointments.Visibility = Visibility.Visible;
					btnProfile.Visibility = Visibility.Collapsed;
					btnAppointmentHistory.Visibility = Visibility.Collapsed;
					btnBookAppointment.Visibility = Visibility.Collapsed;
				}
				else if (_loggedInUser.Role == 2) // Doctor
				{
					txtUserName.Text = $"Xin chào Doctor: {_loggedInUser.Username}";
					btnProfile.Visibility = Visibility.Collapsed;
					btnAppointmentHistory.Visibility = Visibility.Collapsed;
					btnBookAppointment.Visibility = Visibility.Collapsed;
					btnTreatmen.Visibility = Visibility.Visible;
                    btnDoctorProfile.Visibility = Visibility.Visible;
                    btnProfile.Visibility = Visibility.Collapsed;
                }
				else if (_loggedInUser.Role == 3) // Patient
				{
					txtUserName.Text = $"Xin chào Patient: {_loggedInUser.Username}";
                    btnUpdatePassword.Visibility = Visibility.Visible;
                }
			}
			else
			{
				txtUserName.Visibility = Visibility.Collapsed;
				btnLogout.Visibility = Visibility.Collapsed;
				btnLogin.Visibility = Visibility.Visible;
				btnUpdatePassword.Visibility = Visibility.Collapsed;

			
				btnManageAppointments.Visibility = Visibility.Collapsed;
			}
		}


		private void BtnLogout_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
			
				_loggedInUser = null;
				UpdateLoginState();

			}
		}

		private void BookAppointment_Click(object sender, RoutedEventArgs e)
		{
			Appointment booking = new Appointment(_loggedInUser );
			booking.Show();
			this.Hide();
		}

		private void AppointmentHistory_Click(object sender, RoutedEventArgs e)
		{
			History booking = new History(_loggedInUser);
			booking.Show();
			this.Hide();
		}
		private void ListPatient_Click(object sender, RoutedEventArgs e)
		{
			ListPatient list = new ListPatient(_loggedInUser);
			list.Show();
			this.Hide();
		}
		private void ManageAppointments_Click(object sender, RoutedEventArgs e)
		{
			ManagerAppoiment booking = new ManagerAppoiment(_loggedInUser);
			booking.Show();
			this.Hide();
		}

		private void Profile_Click(object sender, RoutedEventArgs e)
		{

			Profile p = new Profile( _loggedInUser);
			p.Show();
			this.Hide();
		}
        private void DoctorProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileDoctor profileDoctor = new ProfileDoctor(_loggedInUser);
            profileDoctor.Show();
            this.Hide();
        }
        private void BtnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ để cập nhật mật khẩu
            UpdatePasswordWindow updatePasswordWindow = new UpdatePasswordWindow(_loggedInUser);
            updatePasswordWindow.ShowDialog();
        }

    }
}
