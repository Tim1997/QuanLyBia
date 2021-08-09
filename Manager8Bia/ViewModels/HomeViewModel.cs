using Manager8Bia.Helpers;
using Manager8Bia.Models;
using Manager8Bia.Services;
using Manager8Bia.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager8Bia.ViewModels
{
    public class HomeViewModel : Domain.BaseViewModel
    {
        #region [Properties]
        private ObservableCollection<Category> _listOptionalItem;
        private string _colorTableOne, _timeTableOne;
        private string _colorTableTwo, _timeTableTwo;
        private string _colorTableThree, _timeTableThree;
        private bool _isTableOpen, _isTableTwoOpen, _isTableThreeOpen, _isTableOneOpen;
        private List<DayHistory> _dayHistories;
        private DayHistory _tableOne, _tableTwo, _tableThree;
        private ClockHelper _timerOne, _timerTwo, _timerThree, _timer;
        private ComboBox _name;
        private TextBox _count;
        private string _currentDescription;
        private int _currentDiscount;
        private int _currentUserPay;
        private int _currentMoney;
        private string _currentTable;


        public ObservableCollection<Category> ListOptionalItem { get => _listOptionalItem; set { _listOptionalItem = value; OnPropertyChanged(); } }
        public string ColorTableOne { get => _colorTableOne; set { _colorTableOne = value; OnPropertyChanged(); } }
        public string ColorTableTwo { get => _colorTableTwo; set { _colorTableTwo = value; OnPropertyChanged(); } }
        public string ColorTableThree { get => _colorTableThree; set { _colorTableThree = value; OnPropertyChanged(); } }
        public bool IsTableOpen { get => _isTableOpen; set { _isTableOpen = value; OnPropertyChanged(); } }
        public string TimeTableOne { get => _timeTableOne; set { _timeTableOne = value; OnPropertyChanged(); } }
        public string TimeTableTwo { get => _timeTableTwo; set { _timeTableTwo = value; OnPropertyChanged(); } }
        public string TimeTableThree { get => _timeTableThree; set { _timeTableThree = value; OnPropertyChanged(); } }
        public DayHistory TableOne { get => _tableOne; set { _tableOne = value; OnPropertyChanged(); } }
        public DayHistory TableTwo { get => _tableTwo; set { _tableTwo = value; OnPropertyChanged(); } }
        public DayHistory TableThree { get => _tableThree; set { _tableThree = value; OnPropertyChanged(); } }
        public string CurrentDescription { get => _currentDescription; set { _currentDescription = value; OnPropertyChanged(); } }
        public int CurrentDiscount { get => _currentDiscount; set { _currentDiscount = value; OnPropertyChanged(); } }
        public int CurrentUserPay { get => _currentUserPay; set { _currentUserPay = value; OnPropertyChanged(); } }
        public int CurrrentMoney { get => _currentMoney; set { _currentMoney = value; OnPropertyChanged(); } }
        public string CurrentTable { get => _currentTable; set { _currentTable = value; OnPropertyChanged(); } }

        #endregion

        #region [Command]
        public ICommand TableOpenCommand { get; set; }
        public ICommand TableShowCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        #endregion

        #region [Service]
        readonly IDataStore<Category> CategoryDataStore;
        readonly IDataStore<DayHistory> DayDataStore;
        readonly IDataStore<MonthHistory> MonthDataStore;
        private double _moneyPerHours;
        private List<Category> Categories;
        #endregion

        public HomeViewModel() { }
        public HomeViewModel(IDataStore<DayHistory> day, IDataStore<MonthHistory> month, IDataStore<Category> cate)
        {
            CategoryDataStore = cate;
            DayDataStore = day;
            MonthDataStore = month;

            Init();

            //////////////////////////////////////

            TableShowCommand = new RelayCommand<string>(p =>
            {
                if (p == TABLE.TableOne.ToStringValue())
                    return IsTableOpen = _isTableOneOpen;
                if (p == TABLE.TableTwo.ToStringValue())
                    return IsTableOpen = _isTableTwoOpen;
                if (p == TABLE.TableThree.ToStringValue())
                    return IsTableOpen = _isTableThreeOpen;

                return false;
            },
            p =>
            {
                _timer?.Close();

                DayHistory table = null;
                if (p == TABLE.TableOne.ToStringValue())
                {
                    table = TableOne;
                }
                if (p == TABLE.TableTwo.ToStringValue())
                {
                    table = TableTwo;
                }
                if (p == TABLE.TableThree.ToStringValue())
                {
                    table = TableThree;
                }

                _timer?.Start(() =>
                {
                    CurrentTable = table.Table;
                    CurrrentMoney = table.Money;
                    CurrentUserPay = table.UserPay;
                    CurrentUserPay = Convert.ToInt32(table.Money * ((100 - CurrentDiscount) * 1.0 / 100));

                    ListOptionalItem.Clear();
                    foreach (var i in table.Categories)
                    {
                        ListOptionalItem.Add(i);
                    }
                }, 1);

            });

            TableOpenCommand = new RelayCommand<string>(p =>
            {
                if (p == TABLE.TableOne.ToStringValue())
                    return !_isTableOneOpen;
                if (p == TABLE.TableTwo.ToStringValue())
                    return !_isTableTwoOpen;
                if (p == TABLE.TableThree.ToStringValue())
                    return !_isTableThreeOpen;

                return false;
            },
            p => TableOpen(p));

            AddCommand = new RelayCommand<string>(p => { return !string.IsNullOrEmpty(p); }, async p =>
            {
                DayHistory table = null;
                if (p == TABLE.TableOne.ToStringValue())
                {
                    table = TableOne;
                }
                if (p == TABLE.TableTwo.ToStringValue())
                {
                    table = TableTwo;
                }
                if (p == TABLE.TableThree.ToStringValue())
                {
                    table = TableThree;
                }

                int count;
                var obj = await DialogHost.Show(new DialogBase("Thêm đồ dùng", ViewAdd()), "RootDialog", delegate (object sender, DialogOpenedEventArgs args) { });

                var isOK = Convert.ToBoolean(obj);
                bool isNumericCount = int.TryParse(_count.Text, out count);

                if (isOK && isNumericCount && count > 0 && _name.SelectedItem != null)
                {
                    var category = Categories?.FirstOrDefault(x => x.Name == _name.SelectedItem.ToString());
                    var item = table.Categories.FirstOrDefault(x => x.Code == _name.SelectedItem.ToString().ToLower().Trim().Replace(" ", ""));

                    // add local data
                    if (item == null)
                    {
                        table.Categories.Add(category);
                    }
                    else
                    {
                        item.Count += count;
                    }

                    // add money
                    table.Money = CurrrentMoney += count * category.Price;
                    table.UserPay = CurrentUserPay = CurrrentMoney;

                    table.Discount = CurrentDiscount;
                    table.Description = CurrentDescription;

                    // add view
                    ListOptionalItem.Clear();
                    foreach (var i in table.Categories)
                    {
                        ListOptionalItem.Add(i);
                    }

                    var data = await DayDataStore.GetItemAsync(table.Id, BaseConstant.PATH_DAY + DateTime.Now.ToString("ddMMyyyy") + ".json");
                    if (data == null)
                        await DayDataStore.AddItemAsync(table, BaseConstant.PATH_DAY + DateTime.Now.ToString("ddMMyyyy") + ".json");
                    else
                    {
                        await DayDataStore.UpdateItemAsync(data, BaseConstant.PATH_DAY + DateTime.Now.ToString("ddMMyyyy") + ".json");
                    }
                }
            });

            OrderCommand = new RelayCommand<string>(p => { return !string.IsNullOrEmpty(p); }, async p =>
            {
                DayHistory table = null;
                if (p == TABLE.TableOne.ToStringValue())
                {
                    table = TableOne;
                }
                if (p == TABLE.TableTwo.ToStringValue())
                {
                    table = TableTwo;
                }
                if (p == TABLE.TableThree.ToStringValue())
                {
                    table = TableThree;
                }

                if (table == null) return;
                // add data
                table.Id = table.Id??Guid.NewGuid().ToString();
                table.TimeEnd = DateTime.Now;
                table.UserPay = CurrentUserPay;
                table.Description = CurrentDescription;
                table.Discount = CurrentDiscount;

                // ghi hoa don theo ngay
                var dName = BaseConstant.PATH_DAY + DateTime.Now.ToString("ddMMyyyy") + ".json";
                if (await DayDataStore.GetItemAsync(table.Id, dName) == null)
                    await DayDataStore.AddItemAsync(table, dName);
                else
                    await DayDataStore.UpdateItemAsync(table, dName);

                // ghi hoa don tong tien theo thang
                var mName = BaseConstant.PATH_MONTH + DateTime.Now.ToString("MMyyyy") + ".json";
                var monthTotal = await MonthDataStore.GetItemAsync(DateTime.Now.Day.ToString(), mName);
                if (monthTotal == null)
                {
                    monthTotal = new MonthHistory
                    {
                        Id = DateTime.Now.Day.ToString(),
                        Money = table.Money,
                    };
                    await MonthDataStore.AddItemAsync(monthTotal, mName);
                }
                else
                {
                    monthTotal.Money += table.Money;
                    await MonthDataStore.UpdateItemAsync(monthTotal, mName);
                } 
                    
                //reset data
                Reset(table.Table);

                //reset timer
                _timerOne?.Close();
                _timer?.Close();
            });
        }

        #region [Init]
        void Init()
        {
            Reset();

            _timer = new ClockHelper();
            _dayHistories = new List<DayHistory>();
            Categories = new List<Category>();

            InitCategoryAsync();
            InitDayHistoryAsync();
            InitMonthHistoryAsync();
        }

        async void InitDayHistoryAsync()
        {
            var dName = BaseConstant.PATH_DAY + DateTime.Now.ToString("ddMMyyyy") + ".json";

            if (!File.Exists(dName))
            {
                _ = await DayDataStore.CreateItemAsync(dName);
            }
            else
            {
                var datas = await DayDataStore.GetItemsAsync(dName);

                for (int i = datas.Count() - 1; i >= 0; i--)
                {
                    var table = datas.ToList()[i];

                    if(table.TimeEnd == DateTime.MinValue)
                    {
                        TableOpen(table);
                    }
                }
            }
        }

        async void InitMonthHistoryAsync()
        {
            var mName = BaseConstant.PATH_MONTH + DateTime.Now.ToString("MMyyyy") + ".json";

            if (!File.Exists(mName))
            {
                _ = await MonthDataStore.CreateItemAsync(mName);
            }
        }

        async void InitCategoryAsync()
        {
            if (File.Exists(BaseConstant.PATH_CATEGORY))
            {
                var result = await CategoryDataStore.GetItemsAsync(BaseConstant.PATH_CATEGORY);

                foreach (var item in result)
                {
                    if (item.Id == BaseConstant.TABLE_ID)
                        _moneyPerHours = item.Price;

                    Categories.Add(item);
                }

            }
            else await DayDataStore.CreateItemAsync(BaseConstant.PATH_CATEGORY);
        }

        void Reset(string table = null)
        {
            if (table == null)
            {
                TimeTableTwo = TimeTableThree = TimeTableOne = "00:00";
                _isTableOneOpen = _isTableTwoOpen = _isTableThreeOpen = IsTableOpen = false;
                ColorTableTwo = ColorTableThree = ColorTableOne = BaseConstant.COLOR_GRAY;
                TableOne = null; TableTwo = null; TableThree = null;
                _timerTwo = new ClockHelper();
                _timerOne = new ClockHelper();
                _timerThree = new ClockHelper();
            }
            else if (TABLE.TableOne.ToStringValue() == table)
            {
                TimeTableOne = "00:00";
                IsTableOpen = _isTableOneOpen = false;
                ColorTableOne = BaseConstant.COLOR_GRAY;
                TableOne = null;
            }
            else if (TABLE.TableTwo.ToStringValue() == table)
            {
                TimeTableTwo = "00:00";
                IsTableOpen = _isTableTwoOpen = false;
                ColorTableTwo = BaseConstant.COLOR_GRAY;
                TableTwo = null;
            }
            else if (TABLE.TableThree.ToStringValue() == table)
            {
                TimeTableThree = "00:00";
                IsTableOpen = _isTableThreeOpen = false;
                ColorTableThree = BaseConstant.COLOR_GRAY;
                TableThree = null;
            }

            CurrentDescription = null;
            CurrentDiscount = 0;
            CurrentTable = null;
            CurrentUserPay = 0;
            CurrrentMoney = 0;
            ListOptionalItem = new ObservableCollection<Category>();
        }
        #endregion


        #region [Method]
        void TableOpen(DayHistory dayStore)
        {
            TableOpen(dayStore.Table, dayStore);
        }

        void TableOpen(string table, DayHistory dayStore = null)
        {
            // check day store
            if (dayStore == null)
            {
                dayStore = new DayHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    TimeStart = DateTime.Now,
                    Table = table,
                    Categories = new List<Category>(),
                    Money = 0,
                };
            }
            else
            {
                foreach (Category item in dayStore.Categories)
                {
                    dayStore.Money = CurrrentMoney += item.Count * item.Price;
                    dayStore.UserPay = CurrentUserPay = CurrrentMoney;

                    CurrentDiscount = dayStore.Discount;
                    CurrentDescription = dayStore.Description;
                }
            }

            #region ban 1
            if (TABLE.TableOne.ToStringValue() == table)
            {
                _timerOne.Start(() =>
                {
                    TimeTableOne = _timerOne.ToString;
                    dayStore.Money = Convert.ToInt32(dayStore.Money + _moneyPerHours / 60);
                });
                ColorTableOne = BaseConstant.COLOR_YELLOW;
                _isTableOneOpen = true;
                TableOne = dayStore;
            }
            #endregion

            #region ban 2
            else if (TABLE.TableTwo.ToStringValue() == table)
            {
                _timerTwo.Start(() =>
                {
                    TimeTableTwo = _timerTwo.ToString;
                    dayStore.Money = Convert.ToInt32(dayStore.Money + _moneyPerHours / 60);
                });
                _isTableTwoOpen = true;
                ColorTableTwo = BaseConstant.COLOR_GREEN;
                TableTwo = dayStore;
            }
            #endregion

            #region ban 3
            else if (TABLE.TableThree.ToStringValue() == table)
            {
                _timerThree.Start(() =>
                {
                    TimeTableThree = _timerThree.ToString;
                    dayStore.Money = Convert.ToInt32(dayStore.Money + _moneyPerHours / 60);
                });
                _isTableThreeOpen = true;
                ColorTableThree = BaseConstant.COLOR_RED;
                TableThree = dayStore;
            }
            #endregion
        }

        UIElement ViewAdd()
        {
            var source = new List<string>();
            foreach (var item in Categories)
            {
                if (item.Id != BaseConstant.TABLE_ID)
                    source.Add(item.Name);
            }

            var wp = new WrapPanel { Width = 200 };

            _name = new ComboBox { Margin = new System.Windows.Thickness(5), Width = 100, ToolTip = "Name object", ItemsSource = source };
            HintAssist.SetHint(_name, "Sản phẩm"); wp.Children.Add(_name);

            _count = new TextBox { Margin = new System.Windows.Thickness(5), Width = 50, Text = "1", ToolTip = "Count object", HorizontalContentAlignment = HorizontalAlignment.Center };
            HintAssist.SetHint(_count, "Số lượng"); wp.Children.Add(_count);
            return wp;
        }
        #endregion
    }
}
