   M S B A M L   `   `   `  ����  BMZSApiHandler    BMZSApiHandler.ApiHandler   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f8metro,http://metro.mahapps.com/winfx/xaml/controls  5        � Title$  Főoldal��5      $	��945��5      $
��1680��5       � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ CanMinimize=�5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $��1��6   $��5��6    ���	#FFCCCCCC$��1,0,0,0q�6u   $��Arial)�6�   ��6I   Z� ��6_   Z� 6�   �� 5   
   -   5         ��Name$ comboBox��5      $��50��6%   $	��200��61   $��0��6=   $��1��6J   $��22(�6Z   
������� � ToolTip$n hTáblák. A lenyitódó fül segéítségével a fodrászat-adatbázis táblái között tudsz váltani.��6�   $��Arial)�6  �� 5   
   -   5         ��Name$ update��5      $��50��6!   $	��200��6-   $��2��69   $��1��6F   $��MÓDOSÍTÁS��6V   $��22(�6j   $��1,0,1,1q�6�   
�������$��Arial)�6�   �� 5   
   -   5        $ delete��5      $��50��6!   $	��200��6-   $��3��69   $��1��6F   $��TÖRLÉS��6V   $��22(�6g   $3 -Törlés gomb. Törli a kiválasztott elemet.��6v   
�������$��Arial)�6�   �� 5   
   $��1��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Center=�6K   .+�5       �� -   5        $	 add��5       $��50��6"   $	��100��6.   $	��ÚJ��6;   $��22(�6H   $C =Hozzáadás gomb. Egy új adat felvitelére ad lehetőséget.��6W   
�������$��Arial)�6�   $��0,0,50,0q�6�   �� 5!      -   5        $ refresh��5!      $��50��6&   $��50��62   $	��↺��6>   $��22(�6J   $��
0,-15,0,-7q�6X   $I CFrissítés gomb. Az adatok ismételt lekérdezését eredményezi.��6�   
�������$��Arial)�6�   5"      �� 5$   
   -   5        $
 exit��5$      $��50��6   $	��200��6+   $��4��67   $��1��6D   $��	KILÉPÉS��6T   $��22(�6f   $3 -Kijelentkezés gomb. Kilép a kezdő oldalra.��6u   
�������$��Arial)�6�   6�   &   System.Windows.Controls.DataGrid  5&   
   -   5        	  Name$	 dataGrid��5&      $��0��6%   $��8��65   
 � UseLayoutRounding
 . $��18(�6�   $��Arial)�6�   5+      5,      