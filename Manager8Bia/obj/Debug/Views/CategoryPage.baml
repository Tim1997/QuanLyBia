   M S B A M L   `   `   `  ����F  BManager8Bia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Manager8Bia.ViewsManager8Bia.Views  5      7 clr-namespace:Manager8Bia.ModelsManager8Bia.Models  5      $    Manager8Bia.Views.CategoryPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Manager8Bia.Views     5      2model clr-namespace:Manager8Bia.Models     5      b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         
CategoryVM  � DataContext#  [  5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   � $��0��6   .+�5      ��    MaterialDesignOutlinedButton#��[ 6    � ToolTip$	 Add��6K   $��Thêm��6Y   $��Right=�5      ��61   �� *
AddCommand+5      �� 5   
   -   5         ��Name$
 list��5       $��1��6   $��Stretch=�5       �� SelectionMode$ Single=�68   ��5   .   �� *ListCategory+ �� Mode$ TwoWay=� �� View 5      �� 5      .P�5      ��  �� CellTemplate 5       �� 5!      .R�5"   "   �� $��25��6(   $��25��63   ��6?   �� *Source+5#      5%      �� 5&      $
��Tên��6%    5'      �� 5(      .R�5)   "   �� $��19(�6,   $��15,0,0,0q�6:   ��6L   �� *Name+5*      5,      �� 5-      $��
Đơn vị��6&    5.      �� 5/      .R�50   "   �� $��19(�6,   $��15,0,0,0q�6:   ��6L   �� *Unit+51      53      �� 54      $��Số lượng��6%    55      �� 56      .R�57   "   �� $��19(�6,   $��15,0,0,0q�6:   ��6L   �� *Count+58      5:      �� 5;      $
��Giá��6%    5<      �� 5=      .R�5>   "   �� $��19(�6,   $��15,0,0,0q�6:   ��6L   �� *Price+5?      5A      �� 5B      $��Tùy chọn��6%    5C      �� 5D      .R�5E   "   ��  �� Orientation$ 
Horizontal=�6-   $��Right=�6F   $	��Top=�5F   -   .+�5G   &   �� -   5          MaterialDesignFlatButton#��[ 5G   -   $
 Edit��5H   )   $��Sửa��68   ��5I   -   �� 	 �� ElementName$
	 list��
 �� Path$
 DataContext.EditCommand���6o   �� $
	 list��$
 SelectedItem�5J   -   �� 5K   &   -   5        #��[ 5K   -   $ Remove��5L   )   $
��Xóa��6:   ��5M   -   �� $
	 list��$
 DataContext.RemoveCommand���6q   �� $
	 list��$
 SelectedItem�5N   -   5O   #   5P      5R      5S      5U      � 5Y   
   $��1��6   .+�5Z      )  #MaterialDesignThemes.Wpf.DialogHost     
Identifier$ 
RootDialog��6(   $��Center=�5[   (   $��Center=�6E   5\      5]      5_      5`      