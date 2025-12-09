using ManagerLibrary.ModelsViews;
using MNHospital_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MNHospital_WPF.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
	{
		private Account _account = new Account();
		private bool _isRegistrationSuccessful = false;

		public Register()
		{
			InitializeComponent();
			this.DataContext = _account;
			this.Closing += Register_Closed;
		}

		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{   
			string password = txtPassword.Password;
			string confirmPassword = txtPasswordC.Password;

			if (string.IsNullOrWhiteSpace(_account.Username ) || string.IsNullOrWhiteSpace(_account.Username) ||
				string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
			{
				MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (password != confirmPassword)
			{
				MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}		
			using (var context = new MnHospitalContext())
			{
				var existingUserByUsername = context.Accounts.SingleOrDefault(u => u.Username == _account.Username);
				if (existingUserByUsername != null)
				{
					MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				var newAccount = new Account
				{
					Username = _account.Username,					
					Userpassword  = password,
					Role  = 3
				};
				ManagerAccount.Instance.AddAccount(newAccount);

				MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
				_isRegistrationSuccessful = true;
				this.Close();
			}
		}

		private void Register_Closed(object sender, EventArgs e)
		{

			Login loginWindow = new Login();
			loginWindow.Show();
		}

		
	}
}

