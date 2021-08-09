using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager8Bia.Domain
{
    public class BaseWindow : BaseBinding
    {
        private string title;

        public string Title { get => title; set { title = value; OnPropertyChanged(); } }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }
    }
}
