   M S B A M L   `   `   `  ����F  BManager8Bia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Manager8Bia.ViewsManager8Bia.Views  5      7 clr-namespace:Manager8Bia.ModelsManager8Bia.Models  5
      #    Manager8Bia.Views.HistoryPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Manager8Bia.Views     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      ,model clr-namespace:Manager8Bia.Models  5
      b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � 	Resources  5         mYellow �� Color 
 mGreen 	 mBlue �� 
TargetType  mTextBlockStyle �� Property �� Value��%�   &	        &	 !     &	 B     &	 c     �� 5   
   $ #fe6e00��6*   �� 5   
   $ #40a33c��6)   �� 5   
   $ #321a92��6(   �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��$ 15(�6)   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��5* �6   �� 5      $��5* �6   .+�5   
   � $��0��6    � RowDefinitions 5       �� 5!      $
��3.5* �6    �� 5"      $
��6.5* �6    .+�5&      � $��0��6    5'      �� 5(      �� 5)      .+�5,      #  MaterialDesignThemes.Wpf.Card  $��0��6&   $��5q�66   .��5-      �  5.      �� 5/      �� 50      .+�54      � $��0��6    55      �� 56   "   $��4* �63   �� 57   "   $��6* �63   .+�5:      � #��[  6#   $��50��6I   .+�5;   "   �� $��/Assets/money.png��6(   $��Bottom=�6C   $��10q�6^   5<      � 5>      $��1��6#   $��0,5,10,0q�63    5?   "   �� 5@   &   �� 5A   &   .+�5D   "   �� $��0��6,    K� Capitals$ AllPetiteCaps=�6R   $��30(�5E   (   #��[  66   $��Bottom=�5F   (   $��Right=�6C   ��5D   9   �� *	NameDay+5F   C   (  "System.Windows.Controls.DatePicker  5G   "   $	��100��6-   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Date��69   $
��auto��6_   $
��Bold"�6m   $��15(�6   $��1��5H   )   $��Right=�66   #��[  6R    ( $MaterialDesignFloatingHintDatePicker#��[ 5I   )   	   SelectedDate	 6g   �� *
DayInMonth+.  (System.Windows.Interactivity.Interaction
   Triggers
 5J   &   /  )System.Windows.Interactivity.EventTrigger  5K   *      	EventName$ SelectedDateChanged��69   .  (System.Windows.Interactivity.TriggerBase   Actions. 5L   .   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6D   �� *
DayCommand+5M   +   5O   #   5P      5Q      � 5U      $��1��6   .+�5V      �� $��1��6(   $��40(�68   #��[  6F   $��Center=�5W   ,   $��Center=�6G   $
��Wrap=�5X   H   ��6,   �� *
MoneyInDay+6H   5Y      5[      5\        5^      $��1��6&   $��5q�66   .��5_      �  5`      �� 5a      �� 5b      .+�5f      � $��0��6    5g      �� 5h   "   $��4* �63   �� 5i   "   $��6* �63   .+�5l      � #��[ 6#   $��50��6H   .+�5m   "   �� $��/Assets/moneybag.png��6(   $��Bottom=�6F   $��10q�6a   5n      � 5p      $��1��6#   $��0,5,10,0q�63   .+�5q   "   �� $��0��6,   $ AllPetiteCaps=�6T   $��30(�5r   (   #��[ 66   $��Bottom=�5s   (   $��Right=�6C   ��5q   9   �� *	NameMonth+5s   C   5t      5u      � 5y      $��1��6   .+�5z      �� $��1��6(   $��40(�68   #��[ 6F   $��Center=�5{   ,   $��Center=�6G   $
��Wrap=�5|   J   ��6,   �� *MoneyInMonth+6J   5}      5      5�      5�      &   System.Windows.Controls.DataGrid  5�      $��1��6   $��0,10,0,0q�6$      AutoGenerateColumns .     
IsReadOnly . ��66   �� *ListDayHistory+ �� SelectedItem 5�   E   �� *SelectItemReceipt+   Columns 5�      0	  *System.Windows.Controls.DataGridTextColumn	  5�      ,
  &System.Windows.Controls.DataGridColumn 
  Header$ Bắt đầu��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *	TimeStart+	  5�      $ Kết thúc��6)    6;   �� *	TimeEnd+	  5�      $
 Bàn��6)    66   �� *Table+	  5�      $ Tiền��6)    67   �� *Money+
 5�        5�      $ SelectionChanged��6%   . 5�         60   �� *ReceiptCommand+5�      5�      5�      � 5�   
   $��1��6   .+�5�      �� $��1��6   $��5q�6C   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$
 Dark=�6N   ��6'   �� *
NamePlay+.��5�      �� .+�5�      � $��Center=�6   $��5q�66   .+�5�      �� $
��Left=�6$   #��[ 6?   $��Tổng thời gian chơi:��5�   $   �� 5�      $��Right=�6$   $��15(�5�   >   #��[ 6L   ��6$   �� *
TimePlay+6L   5�      � 5�      $��Center=�6   $��5q�66   .+�5�      �� $
��Left=�6$   #��[ 6?   $��Tiền chơi:��5�   $   �� 5�      $��Right=�6$   $��15(�5�   ?   #��[ 6M   ��6$   �� *	MoneyPlay+6M   5�        5�      $��1��6   $��0,10,0,0q�6,    .   . ��6>   �� *SelectItemReceipt.Categories+ 5�      	  5�      $
 Tên��61    6>   �� *Name+	  5�      $ Số lượng��61    6C   �� *Count+	  5�      $ 
Đơn giá��61    6B   �� *Price+5�      � 5�      $��Center=�6   $��5q�66   .+�5�      �� $
��Left=�6$   #��[ 6?   $��Giảm giá (%):��5�   $   �� 5�      $��Right=�6$   $��15(�5�   P   #��[ 6^   ��6$   �� *SelectItemReceipt.Discount+6^   5�      �� 5�      $��10q�6    � 5�      $��Center=�6   $��5q�66   .+�5�      �� $
��Left=�6$   #��[ 6?   $��Tổng tiền:��5�   ,   �� 5�      -   5        $��	tbPayment��5�   $   $��Right=�65   #��[ 6Q   ��5�   ,   �� *SelectItemReceipt.Money+5�      � 5�      $��Center=�6   $��5q�66   .+�5�      �� $
��Left=�6$   #��[ 6?   $��Khách thanh toán:��5�   ,   �� 5�      $��Right=�6$   $��15(�5�   O   #��[ 6]   ��6$   �� *SelectItemReceipt.UserPay+6]   5�      �� 5�      $��10q�6    �� 5�       ! MaterialDesignOutlinedTextBox#��[ 6   $
��10,0q�6U   $	��Top=�5�      $	��100��66    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6X   " �� VerticalScrollBarVisibility$
 Auto=�5�      $ Ghi Chú��6A   
�������$
 Dark=�5�   0   ��5�      �� *SelectItemReceipt.Description+5�   0   5�      5�      5�      5�      5�      