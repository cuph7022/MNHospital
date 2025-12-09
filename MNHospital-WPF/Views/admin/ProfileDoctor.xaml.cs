using MNHospital_WPF.Models;
using System;
using System.Linq;
using System.Windows;

namespace MNHospital_WPF.Views.client
{
    public partial class ProfileDoctor : Window
    {
        private Account _loggedInUser;

        public ProfileDoctor(Account loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            this.Closing += ProfileDoctor_Closed;
            LoadProfileData();
        }

        private void LoadProfileData()
        {
            using (var context = new MnHospitalContext())
            {
                var doctor = context.Bacsis
                    .FirstOrDefault(d => d.Username == _loggedInUser.Username);

                if (doctor != null)
                {
                    txtFullName.Text = doctor.Name;
                    txtCd.Text = doctor.Cccd;
                    txtDob.Text = doctor.Dob?.ToString("dd/MM/yyyy") ?? "";
                    txtAddress.Text = doctor.Address;
                    txtPhone.Text = doctor.Phone;
                    txtSpecialization.Text = doctor.Specialization;

                    // Xử lý giới tính (Gender) đúng cách
                    if (doctor.Gender == "Male")
                    {
                        rbtnMale.IsChecked = true;
                    }
                    else if (doctor.Gender == "Female")
                    {
                        rbtnFemale.IsChecked = true;
                    }
                    else if (doctor.Gender == "")
                    {
                        rbtnOther.IsChecked = true;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin bác sĩ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MnHospitalContext())
            {
                // Lấy bác sĩ từ cơ sở dữ liệu
                var doctor = context.Bacsis.FirstOrDefault(d => d.Username == _loggedInUser.Username);

                if (doctor != null)
                {
                    // Cập nhật thông tin bác sĩ
                    doctor.Name = txtFullName.Text;
                    doctor.Cccd = txtCd.Text;
                    doctor.Dob = DateOnly.Parse(txtDob.Text);
                    doctor.Address = txtAddress.Text;
                    doctor.Phone = txtPhone.Text;
                    doctor.Specialization = txtSpecialization.Text;

                    // Cập nhật giới tính
                    if (rbtnMale.IsChecked == true)
                    {
                        doctor.Gender = "Male";
                    }
                    else if (rbtnFemale.IsChecked == true)
                    {
                        doctor.Gender = "Female";
                    }
                    else
                    {
                        doctor.Gender = "Other";
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    MessageBox.Show("Cập nhật hồ sơ thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy bác sĩ để cập nhật.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ProfileDoctor_Closed(object sender, EventArgs e)
        {
            Home home = new Home(_loggedInUser);
            home.Show();
        }
    }
}
