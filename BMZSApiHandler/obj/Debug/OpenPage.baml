   M S B A M L   `   `   `  ����  BMZSApiHandler    BMZSApiHandler.OpenPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f8metro,http://metro.mahapps.com/winfx/xaml/controls  5        � Title$  	Kezdőlap��5      $	��550��5      $	��500��5       � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ CanMinimize=�69   .��5	      �  � RowDefinitions 5
   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $��BMZS FODRÁSZAT��6   
�������$��Center=�6<   $��Center=�6Y   $��50(�6t    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6#      
BlurRadius$ 10Q�61      ShadowDepth$ 3Q�6A      Opacity$ 1Q�6Q   5      �� 5   
   -   5        	 ��Name$	 comboBox��5      $��70��6%   $	��300��61   $��1��6=   $��27(�6J   
�������
 � ToolTip$n
 hTáblák. A lenyitódó fül segéítségével a fodrászat-adatbázis táblái között tudsz váltani.��6k   6�   G� 5   
   -   5         G�Name$
 azon��5      $��2��6$   $	��300��61   $��70��6=   $��27(�6I   $��Center=�6W   $��Center=�6{   �� 5   
   -   5         ��Name$ login��5      $��70��6    $	��300��6,   $��3��68   $��BEJELENTKEZÉS��6E   $��27(�6]   $
 Bejelentkezés gomb.��6k   
�������6�   5      5      