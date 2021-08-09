using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager8Bia.Models
{
    public class MenuItem 
    {
        public string Source { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public MenuItem(string name, string code)
        {
            this.Name = name;
            this.Code = code;
            this.Source = "../Assets/" + Code.ToLower() + ".png";
        }
    }
}
