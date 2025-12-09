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
	/// Interaction logic for Result.xaml
	/// </summary>
	public partial class AddResult : Window
	{
		private Benhnhan _item;
		private Account _loggedInUser;
		public AddResult(Benhnhan item,  Account loggedInUser)
		{
			InitializeComponent();
			_loggedInUser = loggedInUser;
			_item = item;	
			LoadBenh();



		}

		private void LoadBenh()
		{
			using (var context = new MnHospitalContext())
			{
				var kham = context.Benhs.ToList();
				cbBenh.ItemsSource = kham;
			}
		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			
				if (string.IsNullOrWhiteSpace(txtTitle.Text))
				{
					MessageBox.Show("Vui lòng nhập chuẩn đoán.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				if (string.IsNullOrWhiteSpace(txtResult.Text))
				{
					MessageBox.Show("Vui lòng nhập Kết Quả.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				if (cbBenh.SelectedValue == null)
					{
						MessageBox.Show("Vui lòng chọn loại bệnh.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
						return;
					}

				try
				{
					using (var context = new MnHospitalContext())
					{

					var doctor = context.Bacsis
					.FirstOrDefault(b => b.Username == _loggedInUser.Username);

					if (doctor == null)
					{
						MessageBox.Show("Không tìm thấy bác sĩ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}
                    var random = new Random();
                    var result = new Ketqua
                    {
                        Id = random.Next(10000, 99999),
                        Chuandoan = txtTitle.Text,
                        Idbenh = (int)cbBenh.SelectedValue,
                        Ngaykham = DateOnly.FromDateTime(DateTime.Now),
                        Idbenhnhan = _item.Id,
                        Idbacsi = doctor.Id,
                        Ketqua1 = txtResult.Text
                    };

                    ManagerResult.Instance.AddResult(result);
						

						MessageBox.Show("Thêm chuẩn đoán thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
						this.DialogResult = true; 
						this.Close();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Lỗi khi thêm chuẩn đoán: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
	
	}

