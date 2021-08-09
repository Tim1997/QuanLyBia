using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager8Bia.Models
{
    public class DayHistory : Domain.BaseBinding
    {
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        private string table;
        private string description;
        /// <summary>
        /// tổng số tiền nhận được ở bàn này
        /// </summary>
        private int money;
        /// <summary>
        /// các món đặt thêm
        /// </summary>
        public List<Category> Categories { get; set; }
        /// <summary>
        /// giảm giá
        /// </summary>
        private int discount;
        /// <summary>
        /// tiền khách trả
        /// </summary>
        public int UserPay { get; set; }
        public int Money { get => money; set { money = value; OnPropertyChanged(); } }

        public string Table { get => table; set { table = value; OnPropertyChanged(); } }

        public int Discount { get => discount; set { discount = value; OnPropertyChanged(); } }

        public string Description { get => description; set { description = value; OnPropertyChanged(); } }
    }

    public class MonthHistory : Domain.BaseBinding
    {
        public int Money { get; set; }
    }
}
