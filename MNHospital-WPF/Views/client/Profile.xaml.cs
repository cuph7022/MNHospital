using MNHospital_WPF.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MNHospital_WPF.Views.client
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
	{
		private Account _loggedInUser;

		public Profile(Account loggedInUser)
        {
            InitializeComponent();
			_loggedInUser = loggedInUser;
			this.Closing += Profile_Closed;
			LoadProfileData();
		}
		private void LoadProfileData()
		{
			using (var context = new MnHospitalContext())
			{
				var benhnhan = context.Benhnhans
					.FirstOrDefault(b => b.Username == _loggedInUser.Username);

				if (benhnhan == null)
				{
					// Tìm Id lớn nhất và tăng lên 1
					int maxId = context.Benhnhans.Any() 
						? context.Benhnhans.Max(b => b.Id) 
						: 0;
					
					// Tạo bản ghi mới nếu chưa có
					benhnhan = new Benhnhan
					{
						Id = maxId + 1,
						Username = _loggedInUser.Username,
						Name = null,
						Cccd = null,
						Dob = null,
						Address = null,
						Baohiem = null,
						Gender = null
					};
					context.Benhnhans.Add(benhnhan);
					context.SaveChanges();
				}

				// Load dữ liệu vào form
				txtFullName.Text = benhnhan.Name ?? "";
				txtCd.Text = benhnhan.Cccd ?? "";
				txtDob.Text = benhnhan.Dob?.ToString("dd/MM/yyyy") ?? "";
				txtAddress.Text = benhnhan.Address ?? "";
				txtBaohiem.Text = benhnhan.Baohiem ?? "";

				if (benhnhan.Gender == "Male")
				{
					rbtnMale.IsChecked = true;
				}
				else if (benhnhan.Gender == "Female")
				{
					rbtnFemale.IsChecked = true;
				}
				else
				{
					rbtnOther.IsChecked = true;
				}
			}
		}
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MnHospitalContext())
            {
                // Lấy bệnh nhân từ cơ sở dữ liệu
                var benhnhan = context.Benhnhans.FirstOrDefault(b => b.Username == _loggedInUser.Username);

                if (benhnhan == null)
                {
                    // Tìm Id lớn nhất và tăng lên 1
                    int maxId = context.Benhnhans.Any() 
                        ? context.Benhnhans.Max(b => b.Id) 
                        : 0;
                    
                    // Tạo bản ghi mới nếu chưa có
                    benhnhan = new Benhnhan
                    {
                        Id = maxId + 1,
                        Username = _loggedInUser.Username
                    };
                    context.Benhnhans.Add(benhnhan);
                }

                // Cập nhật thông tin bệnh nhân
                benhnhan.Name = string.IsNullOrWhiteSpace(txtFullName.Text) ? null : txtFullName.Text;
                benhnhan.Cccd = string.IsNullOrWhiteSpace(txtCd.Text) ? null : txtCd.Text;
                
                // Xử lý ngày sinh
                if (!string.IsNullOrWhiteSpace(txtDob.Text))
                {
                    if (DateOnly.TryParse(txtDob.Text, out DateOnly dob))
                    {
                        benhnhan.Dob = dob;
                    }
                    else
                    {
                        MessageBox.Show("Định dạng ngày sinh không đúng. Vui lòng nhập theo định dạng dd/MM/yyyy.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    benhnhan.Dob = null;
                }

                benhnhan.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text;
                benhnhan.Baohiem = string.IsNullOrWhiteSpace(txtBaohiem.Text) ? null : txtBaohiem.Text;

                // Cập nhật giới tính
                if (rbtnMale.IsChecked == true)
                {
                    benhnhan.Gender = "Male";
                }
                else if (rbtnFemale.IsChecked == true)
                {
                    benhnhan.Gender = "Female";
                }
                else
                {
                    benhnhan.Gender = "Other";
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                context.SaveChanges();

                MessageBox.Show("Cập nhật hồ sơ thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void Profile_Closed(object sender, EventArgs e)
		{

			Home home = new Home(_loggedInUser);
			home.Show();
		}
	}
}

