using Manager8Bia.Models;
using Manager8Bia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager8Bia.ViewModels
{
    public class HistoryViewModel : Domain.BaseViewModel
    {
        #region [Properties]
        private ObservableCollection<DayHistory> _listDayHistory;
        private DateTime _dayInMonth;
        private string _nameDay, _nameMonth;
        private string _moneyInDay, _moneyInMonth;
        private DateTime _now;
        private CultureInfo cul;
        private List<MonthHistory> _months;
        private DayHistory _selectItemReceipt;
        private string _moneyPlay;
        private string _timePlay;
        private string _namePlay;
        private int _moneyPerHours;

        public ObservableCollection<DayHistory> ListDayHistory { get => _listDayHistory; set { _listDayHistory = value; OnPropertyChanged(); } }
        public DateTime DayInMonth { get => _dayInMonth; set { _dayInMonth = value; OnPropertyChanged(); } }
        public string NameDay { get => _nameDay; set { _nameDay = value; OnPropertyChanged(); } }
        public string NameMonth { get => _nameMonth; set { _nameMonth = value; OnPropertyChanged(); } }
        public string MoneyInDay { get => _moneyInDay; set { _moneyInDay = value; OnPropertyChanged(); } }
        public string MoneyInMonth { get => _moneyInMonth; set { _moneyInMonth = value; OnPropertyChanged(); } }
        public DayHistory SelectItemReceipt { get => _selectItemReceipt; set { _selectItemReceipt = value; OnPropertyChanged(); } }
        public string MoneyPlay { get => _moneyPlay; set { _moneyPlay = value; OnPropertyChanged(); } }
        public string TimePlay { get => _timePlay; set { _timePlay = value; OnPropertyChanged(); } }
        public string NamePlay { get => _namePlay; set { _namePlay = value; OnPropertyChanged(); } }

        #endregion

        #region [Command]
        public ICommand DayCommand { get; set; }
        public ICommand ReceiptCommand { get; set; }


        #endregion

        #region [Service]
        static ProviderDataStore<Category> CategoryDataStore = new ProviderDataStore<Category>();
        static ProviderDataStore<DayHistory> DayDataStore = new ProviderDataStore<DayHistory>();
        static ProviderDataStore<MonthHistory> MonthDataStore = new ProviderDataStore<MonthHistory>();
        #endregion

        public HistoryViewModel()
        {
            Init();

            //////////////////////////////////////////ReceiptCommand

            DayCommand = new RelayCommand<object>(p => { return true; }, p => 
            {
                NameDay = BaseConstant.DAYOFWEEKS[DayInMonth.DayOfWeek.ToString()];
                var day = _months.FirstOrDefault(x => x.Id == DayInMonth.Day.ToString());
                MoneyInDay = day == null? "N/A" : day.Money.ToString("#,###", cul.NumberFormat);

                SelectItemReceipt = null;
                TimePlay = MoneyPlay = NamePlay = null;
                InitDayHistoryAsync(DayInMonth.Day.ToString());
            });

            ReceiptCommand = new RelayCommand<object>(p => { return SelectItemReceipt != null; }, p =>
            {
                var t = new DateTime((SelectItemReceipt.TimeEnd - SelectItemReceipt.TimeStart).Ticks);
                TimePlay = t.ToString("HH:mm");

                var m = (_moneyPerHours * 1.0 / 60) * t.Minute + _moneyPerHours * t.Hour;
                MoneyPlay = m.ToString("#,###", cul.NumberFormat);
                NamePlay = $"Hóa đơn {SelectItemReceipt.Table}";
            });
        }


        #region [Init]
        void Init()
        {
            _now = DateTime.Now;
            ListDayHistory = new ObservableCollection<DayHistory>();
            cul = CultureInfo.GetCultureInfo("en-US");

            Reset();
            InitMoneyAsync(_now.Day.ToString());
            InitDayHistoryAsync(_now.Day.ToString());
            InitCategoryAsync();
        }

        void Reset()
        {
            NameDay = BaseConstant.DAYOFWEEKS[_now.DayOfWeek.ToString()];
            NameMonth = $"Tháng {_now.Month}";
            DayInMonth = _now;
        }

        async void InitMoneyAsync(string day)
        {
            var mName = BaseConstant.PATH_MONTH + DateTime.Now.ToString("MMyyyy") + ".json";

            if (File.Exists(mName))
            {
                _months = (await MonthDataStore.GetItemsAsync(mName)).ToList();

                var moneyMonth = 0;
                foreach (var item in _months)
                {
                    moneyMonth += item.Money;

                    // get money in day
                    if (item.Id == day)
                        MoneyInDay = item.Money.ToString("#,###", cul.NumberFormat);
                }

                MoneyInMonth = moneyMonth.ToString("#,###", cul.NumberFormat);
            }
        }

        async void InitDayHistoryAsync(string day)
        {
            var dName = BaseConstant.PATH_DAY + day + DateTime.Now.ToString("MMyyyy") + ".json";
            ListDayHistory.Clear();

            if (File.Exists(dName))
            {
                var list = await DayDataStore.GetItemsAsync(dName);

                foreach (var item in list)
                {
                    ListDayHistory.Add(item);
                }
            }
        }

        async void InitCategoryAsync()
        {
            if (File.Exists(BaseConstant.PATH_CATEGORY))
            {
                var result = await CategoryDataStore.GetItemAsync(BaseConstant.TABLE_ID, BaseConstant.PATH_CATEGORY);
                _moneyPerHours = result.Price;
            }
        }
        #endregion

        #region [Method]
        #endregion
    }
}
