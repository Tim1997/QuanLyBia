using Manager8Bia.Domain;
using Manager8Bia.Models;
using Manager8Bia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager8Bia.ViewModels
{
    public class MainViewModel : Domain.BaseViewModel
    {
        #region [Properties]
        private ObservableCollection<MenuItem> _listMenu;
        private Domain.BaseViewModel _currentViewModel;


        public ObservableCollection<MenuItem> ListMenu { get => _listMenu; set { _listMenu = value; OnPropertyChanged(); } }
        public BaseViewModel CurrentViewModel { get => _currentViewModel; set { _currentViewModel = value; OnPropertyChanged(); } }

        #endregion

        #region [Command]
        public ICommand MenuCommand { get; set; }
        #endregion

        public MainViewModel(IDataStore<DayHistory> day, IDataStore<MonthHistory> month, IDataStore<Category> cate)
        {
            Init(day, month, cate);

            //////////////////////////////////////////

            MenuCommand = new RelayCommand<MenuItem>(p => { return true; }, p => LoadPage(p.Code, day, month, cate));
        }


        #region [Init]
        void Init(IDataStore<DayHistory> day, IDataStore<MonthHistory> month, IDataStore<Category> cate)
        {
            Title = "8 Balls";
            ListMenu = new ObservableCollection<MenuItem>() { new MenuItem("Trang chủ", "home"), new MenuItem("Danh mục", "category"), new MenuItem("Lịch sử", "history") };
            CurrentViewModel = new HomeViewModel(day, month, cate) { Title = "Home" };
        }
        #endregion

        #region [Method]
        private void LoadPage(string name, IDataStore<DayHistory> day, IDataStore<MonthHistory> month, IDataStore<Category> cate)
        {
            if(name.ToLower() == "category")
                CurrentViewModel = new CategoryViewModel { Title = "CategoryViewModel" };
            else if(name.ToLower() == "history")
                CurrentViewModel = new HistoryViewModel { Title = "HistoryViewModel" };
            else 
                CurrentViewModel = new HomeViewModel(day, month, cate) { Title = "HomeViewModel" };
        }
        #endregion
    }
}
