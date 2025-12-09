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

namespace MNHospital_WPF.Views.client
{
	/// <summary>
	/// Interaction logic for History.xaml
	/// </summary>
	public partial class History : Window
	{
		private Account _loggedInUser;
		public History(Account loggedInUser)
		{
			InitializeComponent();
			_loggedInUser = loggedInUser;
			this.Closing += Profile_Closed;
			LoadHistoryData();
		}

        private void LoadHistoryData()
        {
            using (var context = new MnHospitalContext())
            {
                var historyData = (from k in context.Ketquas
                                   join b in context.Benhs on k.Idbenh equals b.Id
                                   join p in context.Benhnhans on k.Idbenhnhan equals p.Id
                                   join l in context.Datliches on p.Id equals l.Idbenhnhan
                                   join kh in context.Khams on l.Idkham equals kh.Id
                                   join d in context.Bacsis on k.Idbacsi equals d.Id
                                   where l.Status == (int)OrderStatus.Confirmed
                                   group new { k, b, d, kh, l } by new
                                   {
                                       k.Ngaykham,
                                       DoctorName = d.Name,
                                       ServiceName = kh.Name,
                                       AppointmentTime = l.Giokham
                                   } into g
                                   select new
                                   {
                                       AppointmentDate = g.Key.Ngaykham ?? DateOnly.FromDateTime(DateTime.Now),
                                       DoctorName = g.Key.DoctorName,
                                       ServiceName = g.Key.ServiceName,
                                       Diagnosis = g.First().k.Chuandoan,
                                       Disease = g.First().b.Name,
                                       Price = g.First().kh.Price,
                                       KQ = g.First().k.Ketqua1,
                                       AppointmentTime = g.Key.AppointmentTime
                                   }).ToList();

                HistoryDataGrid.ItemsSource = historyData;
            }
        }
        private void Profile_Closed(object sender, EventArgs e)
		{

			Home home = new Home(_loggedInUser);
			home.Show();
		}
	}
}
