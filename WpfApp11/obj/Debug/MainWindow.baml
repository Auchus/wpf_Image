   M S B A M L   `   `   `  ����C  ?WpfApp11, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfApp11WpfApp11  5   	       WpfApp11.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfApp11     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �� .+�5
   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   $��0��6!   $��Product Name:��61   �� 5      -   5         ��Name$ txtProductName��5   3   $��0��6   $��1��6#   63   �� 5      $��1��6   $��0��6!   $��Product Image:��61   �� 5      -   5         ��Name$ btnUploadImage��5   2   $��1��6   $��1��6"   $��Upload Image��6J   6a   �� 5      -   5         ��Name$ imgProductImage��5   1   $��2��6   $��1��6!   61   �� 5      -   5        $ btnSave��5   2   $��3��6   $��1��6"   $
��Save��6C   6R   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
dgProducts��5         AutoGenerateColumns .     
IsReadOnly . ��6C   �� 	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Product Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *ProductName+4  .System.Windows.Controls.DataGridTemplateColumn  5       $
 Product Image��6)      CellTemplate 5!      �� 5"      .R�5#      �� $	��100��6D   ��6$   �� *ProductImage+6D   5$      5&        5'      $

 Edit��6)    5(      �� 5)      .R�5*      �� -   5        $
��Edit��5*   %   64   5+      5-        5.      $
 Delete��6)    5/      �� 50      .R�51      �� -   5        $��Delete��51   %   66   52      54      56      57      58      