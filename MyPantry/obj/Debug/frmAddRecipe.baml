   M S B A M L   `   `   `  ����C  ?MyPantry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MyPantryMyPantry  5   	       MyPantry.frmAddRecipe   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MyPantry     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
Add Recipe��5	   	   $	��200��6   $	��400��6)    � 	Resources 5       �� 
TargetType   AllButtonStyle �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value��%  &	        �� 5   
   " ��6   .�5      ��  � ��$ 15(�5      �� 5       � ��$
 Bold"�5      �� 5       �@�Cursor$
 Hand��5      �� 5       � ��
 ��ح�5      �� 5       � ��$ Cooper Black)�5      �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��5   !   ._�5      � .+�5    "   �� $ ButtonContentPresenter��63   #��z��5!   3   $��Center=�5"   3   $��Center=�5#   3   5$      5%      	 5'      P� 5(      
 �@J�IsMouseOver . . �5*   "   ��  � ��
 �����5+   )   5,      P� 5.      
 � �� . . �50   "   ��  � ��
 �����51   )   �� 52   "    � ��
 �����53   )   54      56      58      59      5:      .��5<      � 
������� � ColumnDefinitions 5>   
   �� 5?      �� 5@       � RowDefinitions 5B   
   �� 5C      $��45 �6   �� 5D      �� 5E      $��40 �6   �� 5F      $��10 �6   .+�5K   
   �� $��Center=�6   $��Center=�5L      $��2��5M      $
��Bold"�5N      $��Cooper Black)�5O      $��20(�5P      What is your recipe called?6?   �� 5R   
   $��Center=�6   $��Bottom=�5S      $��2��5T      $��1��5U      $��10(�5V      $
��Bold"�5W      $��Cooper Black)�5X      .��6*   Max Length: 2556;   �� 5Z   
   -   5         ��Name$ tbxRecipeName��5Z      $��Center=�5[      $��Center=�5\      $��1��5]      $��2��5^      $	��300��5_      $��40��5`       �� TextWrapping$ NoWrap=�5a       �� 	MaxLength$	 255��5b      $��Center=�5c      $��Center=�5d      $��20(�5e      $��Cooper Black)�5f      $
��Bold"�5g      6&   �� 5k   
   -   5         ��Name$ btnAdd��5k      #��[  5l      $��0 5 13 5q�5m      $��2��5n      $��90��5o      $��Right=�5p      .��6B   Add6G   �� 5r   
   -   5        $ 	btnCancel��5r      #��[  5s      $��13 5 0 5q�5t      $��2��5u      $��90��5v      $
��Left=�5w      $��1��5x      .��5y   )   Cancel61   5}      5~      