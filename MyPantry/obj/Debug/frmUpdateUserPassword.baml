   M S B A M L   `   `   `  ����C  ?MyPantry, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MyPantryMyPantry  5   	   $    MyPantry.frmUpdateUserPassword   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:MyPantry     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Update Password��5	   	   $	��300��6!   $	��450��6.    � 	Resources 5       �� 
TargetType   AllButtonStyle �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value��%2  &	        �� 5   
   " ��6   .�5      ��  � ��$ 15(�5      �� 5       � ��$
 Bold"�5      �� 5       �@�Cursor$
 Hand��5      �� 5       � ��
 ��ح�5      �� 5       � ��$ Cooper Black)�5      �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��5   !   ._�5       � .+�5!   "   �� $ ButtonContentPresenter��63   #��z��5"   3   $��Center=�5#   3   $��Center=�5$   3   5%      5&      	 5(      P� 5)      
 �@J�IsMouseOver . . �5+   "   ��  � �� �#E594005,   )   �� 5-   "    � ��
 �����5.   )   5/      P� 51      
 � �� . . �53   "   ��  � ��
 �����54   )   �� 55   "    � ��
 �����56   )   57      59      5;      5<      5=      .��5@      � 
������� � RowDefinitions 5B   
   �� 5C      $��30 �6   �� 5D      �� 5E      �� 5F      �� 5G      �� 5H      $��40 �6    � ColumnDefinitions 5L   
   �� 5M      �� 5N      $	��300 �6   .+�5S   
   �� -   5         ��Name$ tbkChangePassword��5S      $��Cooper Black)�5T      $��15(�5U      $��Center=�5V      $��Center=�5W      $��2��5X      Change Your Password!6;   �� 5Z   
   $��1��6   $��Cooper Black)�5[      $��Center=�5\      $��Right=�5]      .��6,   Email Address:6<   �� 5_   
   $��2��6   $��Cooper Black)�5`      $��Center=�5a      $��Right=�5b      .��6,   Current Password:6?   �� 5d   
   $��3��6   $��Cooper Black)�5e      $��Center=�5f      $��Right=�5g      .��6,   New Password:6;   �� 5i   
   $��4��6   $��Cooper Black)�5j      $��Center=�5k      $��Right=�5l      .��6,   Re-enter Password:6@   �� 5p   
   -   5         ��Name$ txtEmail��5p      $
��Bold"�5q      $��Center=�5r      $��Center=�5s      $��1��5t      $��1��5u      $��5 5 5 5q�5v      G� 5x   
   -   5         G�Name$ pwdOldPassword��5x      $��Center=�5y      $��Center=�5z      $��2��5{      $��1��5|      $��5 5 5 5q�5}      G� 5   
   -   5        $ pwdNewPassword��5      $��Center=�5�      $��Center=�5�      $��3��5�      $��1��5�      $��5 5 5 5q�5�      G� 5�   
   -   5        $ pwdRetypePassword��5�      $��Center=�5�      $��Center=�5�      $��4��5�      $��1��5�      $��5 5 5 5q�5�      �� 5�   
   -   5         ��Name$ btnSubmitPasswordChange��5�      #��[  5�      $��6��5�      $��70��5�      $��Right=�5�      $��0 5 10 5q�5�      $��Submit��5�      6J   �� 5�   
   -   5        $ btnCancelPasswordChange��5�      #��[  5�      $��6��5�      $��1��5�      $��70��5�      $
��Left=�5�      $��10 5 0 5q�5�      $��Cancel��5�      6J   5�      5�      