using System;
using System.Collections.Generic;

namespace MNHospital_WPF.Models
{
    public partial class Bacsi
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Name { get; set; }

        public string? Cccd { get; set; } 

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public DateOnly? Dob { get; set; }

        public int? Khoa { get; set; }

        public string? Phone { get; set; } // Số điện thoại bác sĩ

        public string? Specialization { get; set; } // Chuyên môn bác sĩ

        public virtual ICollection<Ketqua> Ketquas { get; set; } = new List<Ketqua>();

        public virtual Khoakham? KhoaNavigation { get; set; }

        public virtual Account? UsernameNavigation { get; set; }
    }
}
