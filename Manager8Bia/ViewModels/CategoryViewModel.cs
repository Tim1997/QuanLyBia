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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Manager8Bia.ViewModels
{
    public class CategoryViewModel : Domain.BaseViewModel
    {
        #region [Properties]
        private ObservableCollection<Category> _listCategory;
        private TextBox _name;
        private TextBox _count;
        private TextBox _price;
        private ComboBox _unit;

        public ObservableCollection<Category> ListCategory { get => _listCategory; set { _listCategory = value; OnPropertyChanged(); } }
        #endregion

        #region [Command]
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        #endregion

        #region [Service]
        static ProviderDataStore<Category> DataStore = new ProviderDataStore<Category>();
        #endregion

        public CategoryViewModel()
        {
            InitAsync();

            //////////////////////////////////////

            AddCommand = new RelayCommand<object>(p => { return true; }, async p =>
            {
                int count; int price;
                var obj = await DialogHost.Show(new DialogBase("Thêm", ViewAdd()), "RootDialog", delegate (object sender, DialogOpenedEventArgs args) {});

                var isOK = Convert.ToBoolean(obj);
                bool isNumericCount = int.TryParse(_count.Text, out count);
                bool isNumericPrice = int.TryParse(_price.Text, out price);

                if (isOK && isNumericCount && isNumericPrice && _unit.SelectedItem != null && count > 0 && price > 0)
                {
                    if (ListCategory.Any(x => x.Code == _name.Text?.ToLower().Trim().Replace(" ", ""))) return;

                    var item = new Category(_name.Text, count, price, _unit.SelectedItem.ToString());
                    //add view
                    ListCategory.Add(item);

                    //add data store
                    await DataStore.AddItemAsync(item, BaseConstant.PATH_CATEGORY);
                }
            });

            EditCommand = new RelayCommand<Category>(p => { return p != null; }, async p =>
            {
                int count; int price;
                var obj = await DialogHost.Show(new DialogBase("Sửa", ViewAdd(p.Name, p.Count, p.Price, p.Unit)), "RootDialog", delegate (object sender, DialogOpenedEventArgs args) { });

                var isOK = Convert.ToBoolean(obj);
                bool isNumericCount = int.TryParse(_count.Text, out count);
                bool isNumericPrice = int.TryParse(_price.Text, out price);

                if (isOK && isNumericCount && isNumericPrice && _unit.SelectedItem != null)
                {
                    //update view
                    var item = ListCategory.FirstOrDefault(x => x.Code == p.Code);
                    if (item != null)
                    {
                        item.Name = _name.Text;
                        item.Count = count;
                        item.Price = price;
                        item.Unit = _unit.SelectedItem.ToString();
                    }

                    //update data store
                    await DataStore.UpdateItemAsync(item, BaseConstant.PATH_CATEGORY);
                }
            });

            RemoveCommand = new RelayCommand<Category>(p => 
            {
                if (p == null) return false;
                if (p.Id == BaseConstant.TABLE_ID) return false;

                return true; 
            }, async p =>
            {
                //remove view
                var item = ListCategory.FirstOrDefault(x => x.Code == p.Code);
                if (item != null)
                {
                    ListCategory.Remove(item);
                }

                //remove data store
                await DataStore.DeleteItemAsync(item.Id, BaseConstant.PATH_CATEGORY);

            });
        }

        #region [Init]
        async void InitAsync()
        {
            ListCategory = new ObservableCollection<Category>();

            if (File.Exists(BaseConstant.PATH_CATEGORY))
            {
                var result = await DataStore.GetItemsAsync(BaseConstant.PATH_CATEGORY);
                foreach (var item in result)
                {
                    ListCategory.Add(item);
                }
            }
            else await DataStore.CreateItemAsync(BaseConstant.PATH_CATEGORY);
        }
        #endregion


        #region [Method]
        UIElement ViewAdd(string name = null, int count = 1, double price = -1, string unit = null)
        {
            var st = new StackPanel { Width = 200 };

            _name = new TextBox { Margin = new System.Windows.Thickness(5), Width = 200, ToolTip = "Name object" , };
            HintAssist.SetHint(_name, "Tên sản phẩm"); st.Children.Add(_name);

            InputScope scope = new InputScope();
            InputScopeName scopeName = new InputScopeName();
            scopeName.NameValue = InputScopeNameValue.Number;
            scope.Names.Add(scopeName);

            _count = new TextBox { Margin = new System.Windows.Thickness(5), Width = 200, ToolTip = "Count object", InputScope = scope, HorizontalContentAlignment = HorizontalAlignment.Center };
            HintAssist.SetHint(_count, "Số lượng"); st.Children.Add(_count);
            _price = new TextBox { Margin = new System.Windows.Thickness(5), Width = 200, ToolTip = "Price object", InputScope = scope };
            HintAssist.SetHint(_price, "Giá"); st.Children.Add(_price);

            _unit = new ComboBox { Margin = new System.Windows.Thickness(5), Width = 200, ToolTip = "Unit object", ItemsSource = BaseConstant.UNITS.Values.ToList()};
            HintAssist.SetHint(_unit, "Đơn vị"); st.Children.Add(_unit);

            _name.Text = name; _count.Text = count == -1 ? null : count.ToString();
            _unit.Text = unit; _price.Text = price == -1 ? null : price.ToString();
            return st;
        }

        #endregion
    }
}
