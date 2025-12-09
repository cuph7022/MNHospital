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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.ApplicationServices;
using MNHospital_WPF.Models;

namespace MNHospital_WPF.Views.client
{
    public partial class UpdatePasswordWindow : Window
    {
        private Account _loggedInUser;

        public UpdatePasswordWindow(Account loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
        }

        private void BtnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtOldPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            using (var context = new MnHospitalContext())
            {
                var user = context.Accounts.FirstOrDefault(a => a.Username == _loggedInUser.Username);

                if (user != null)
                {
                    if (user.Userpassword != oldPassword)
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (newPassword != confirmPassword)
                    {
                        MessageBox.Show("Mật khẩu mới không khớp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        user.Userpassword = newPassword;
                        context.SaveChanges();
                        MessageBox.Show("Mật khẩu đã được cập nhật thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy người dùng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
