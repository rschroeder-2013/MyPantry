   M S B A M L   `   `   `  ����C  ?MyPantry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MyPantryMyPantry  5   	       MyPantry.frmAddEditRecipe   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MyPantry     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Recipe Ingredients��5	   	   $	��450��6$   $	��600��61    � 	Resources 5       �� 
TargetType   AllButtonStyle �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value  AllDataGridStyle&   System.Windows.Controls.DataGrid��%�  &	        &	 (    �� 5   
   " ��6   .�5      ��  � ��$ 15(�5      �� 5       � ��$
 Bold"�5      �� 5       �@�Cursor$
 Hand��5      �� 5       � ��
 ��ح�5      �� 5       � ��$ Cooper Black)�5      �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��5   !   ._�5      � .+�5   "   �� $ ButtonContentPresenter��63   #��z��5    3   $��Center=�5!   3   $��Center=�5"   3   5#      5$      	 5&      P� 5'      
 �@J�IsMouseOver . . �5)   "   ��  � �� �#E594005*   )   �� 5+   "    � ��
 �����5,   )   5-      P� 5/      
 � �� . . �51   "   ��  � ��
 �����52   )   �� 53   "    � ��
 �����54   )   55      57      59      5:      �� 5<   
   "  6)   .�5=      ��  � ��$
 Bold"�6+   �� 5>       � ��$ Cooper Black)�6+   �� 5?       � ��$ 17(�6)   5@      5A      .��5E      � 
������� � RowDefinitions 5G   
   �� 5H      $��50 �6   �� 5I      $��30 �6   �� 5J      �� 5K      $��40 �6   �� 5L      $��20 �6    � ColumnDefinitions 5N   
   �� 5O      �� 5P      .+�5U   
   �� -   5         ��Name$ tblkRecipeName��5U      $��2��5V      $��Center=�5W      $��Center=�5X      $��30(�5Y      $
��Bold"�5Z      $��Cooper Black)�5[      Recipe Name6;   �� 5]   
   -   5         ��Name$ lblRecipeID��5]      $��2��5^      $��1��5_      $��	Recipe ID��5`      $��Center=�5a      $��Center=�5b      $
��Bold"�5c      $��Cooper Black)�5d      $��20(�5e        5i   
   -   5          Name$ dgRecipeIngredients��5i      $��2��5j      $��2��5k         
IsReadOnly . #��[ 5m         SelectionMode$ Single=�5n      �� 5r   
   -   5         ��Name$ 
btnRefresh��5r      #��[  5s      $��13 5 5 5q�5t      $��3��5u      $��90��5v      $
��Left=�5w      $��0��5x      .��5y      	Refresh6   �� 5{   
   -   5        $ btnAddRecipeIngredient��5{      #��[  5|      $��0 5 13 5q�5}      $��3��5~      $	��150��5      $��Right=�5�      .��5�   6   Add Ingredient6F   �� 5�   
   -   5        $ btnDeleteRecipeIngredient��5�      #��[  5�      $��13 5 0 5q�5�      $��3��5�      $	��150��5�      $
��Left=�5�      $��1��5�      .��5�   9   Delete Ingredient6L   �� 5�   
   -   5        $ 	btnCancel��5�      #��[  5�      $��13 5 5 5q�5�      $��3��5�      $��90��5�      $��Right=�5�      $��1��5�      .��5�   )   Cancel61   5�      5�      