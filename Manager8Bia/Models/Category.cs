using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager8Bia.Models
{
    public class Category : Domain.BaseBinding
    {
        public string Code { get; set; }
        private string name;
        private int price;
        private int count;
        public string Source { get; set; }
        private string unit;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        public int Price { get => price; set { price = value; OnPropertyChanged(); } }
        public int Count { get => count; set { count = value; OnPropertyChanged(); } }
        public string Unit { get => unit; set { unit = value; OnPropertyChanged(); } }

        public Category() { Id = Guid.NewGuid().ToString(); }
        public Category(string name, int count, int price, string unit )
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Count = count;
            Price = price;
            Unit = unit;
            Code = Name?.ToLower().Trim().Replace(" ", "");
            Source = "../Assets/" + BaseConstant.UNITS.FirstOrDefault(x => x.Value == Unit).Key + ".png";
        }
    }
}
