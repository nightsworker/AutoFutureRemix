   M S B A M L   `   `   `  ����F  BAutoFutures, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AutoFuturesAutoFutures  5          AutoFutures.MASet   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:AutoFutures     5      .��5	      � -   5          �Name$  MAGrid��5	      $y�Visible=�6   $��40��60   $��10,5,0,0q�6<   $	��Top=�6N   ���	#FF353535$
��Left=�6}   $
��1450��6�    � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   .+�5"   
   �� -   5         ��Name$ 均線��5"       $��0��6   $��均線��6,   $
��Left=�69   $��Center=�6T   $
��Bold"�6o   $��30��6�   $��40��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��0,0,0,0q�6�   ���	#FFFFCA006�   �� 5#   
   -   5         ��Name$ 均線數值��5#   "   $��1��6   $
��Left=�60   $��30��6K    �� TextWrapping$
 Wrap=�6W   $��0��6k   $��Center=�6t   $��50��6�   $��Center=�6�    J� 	AllowDrop$ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6#  ���	#FF656565
�������
���   �6_  �� 5$   
   -   5        $ 大台均線��5$       $��2��6   $��大台均線��6.   $
��Left=�6=   $��Center=�6X   $
��Bold"�6s   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006   �� 5%   
   -   5        $ 大台均線數值��5%   "   $��3��6   $
��Left=�62   $��30��6M   $
 Wrap=�6Y   $��0��6m   $��Center=�6v   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6%  ���	#FF656565
�������
���   �6a  �� 5&   
   -   5        $ 大台價差��5&       $��4��6   $��大台價差��6.   $
��Left=�6=   $��Center=�6X   $
��Bold"�6s   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006   �� 5'   
   -   5        $ 大台價差數值��5'   "   $��5��6   $
��Left=�62   $��30��6M   $
 Wrap=�6Y   $��0��6m   $��Center=�6v   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6%  ���	#FF656565
�������
���   �6a  �� 5(   
   -   5        $ 	乖離率��5(       $��6��6   $��	乖離率��6-   $
��Left=�6;   $��Center=�6V   $
��Bold"�6q   $��30��6�   $��55��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006�   �� 5)   
   -	   5        $ 乖離率數值��5)   "   $��7��6   $
��Left=�61   $��30��6L   $
 Wrap=�6X   $��0��6l   $��Center=�6u   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6$  ���	#FF656565
�������
���   �6`  �� 5*   
   -
   5        $ 停利均線��5*       $��8��6   $��停利均線��6.   $
��Left=�6=   $��Center=�6X   $
��Bold"�6s   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006   �� 5+   
   -   5        $ 停利均線數值��5+   "   $��9��6   $
��Left=�62   $��30��6M   $
 Wrap=�6Y   $��0��6m   $��Center=�6v   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6%  ���	#FF656565
�������
���   �6a  �� 5,   
   -   5        $ 停利乖離率��5,   !   $��10��6   $��	乖離率��60   $
��Left=�6>   $��Center=�6Y   $
��Bold"�6t   $��30��6�   $��55��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006  �� 5-   
   -   5        $ 停利乖離率數值��5-   #   $��11��6   $
��Left=�64   $��30��6O   $
 Wrap=�6[   $��0��6o   $��Center=�6x   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6'  ���	#FF656565
�������
���   �6c  �� 5.   
   -   5        $ 停利價格��5.   !   $��12��6   $��停利價格��6/   $
��Left=�6>   $��Center=�6Y   $
��Bold"�6t   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006  �� 5/   
   -   5        $ 停利價格數值��5/   #   $��13��6   $
��Left=�63   $��30��6N   $
 Wrap=�6Z   $��0��6n   $��Center=�6w   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6&  ���	#FF656565
�������
���   �6b  �� 50   
   -   5        $ 停利價差��50   !   $��14��6   $��停利價差��6/   $
��Left=�6>   $��Center=�6Y   $
��Bold"�6t   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006  �� 51   
   -   5        $ 停利價差數值��51   #   $��15��6   $
��Left=�63   $��30��6N   $
 Wrap=�6Z   $��0��6n   $��Center=�6w   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6&  ���	#FF656565
�������
���   �6b  �� 52   
   -   5        $ 停利距離��52   !   $��16��6   $��停利距離��6/   $
��Left=�6>   $��Center=�6Y   $
��Bold"�6t   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006  �� 53   
   -   5        $ 停利距離數值��53   #   $��17��6   $
��Left=�63   $��30��6N   $
 Wrap=�6Z   $��0��6n   $��Center=�6w   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6&  ���	#FF656565
�������
���   �6b  �� 54   
   -   5        $ 均線預估��54   !   $��18��6   $��均線預估��6/   $
��Left=�6>   $��Center=�6Y   $
��Bold"�6t   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006  �� 55   
   -   5        $ 均線預估數值��55   #   $��19��6   $
��Left=�63   $��30��6N   $
 Wrap=�6Z   $��0��6n   $��Center=�6w   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6&  ���	#FF656565
�������
���   �6b  �� 56   
   -   5        $ 均線移動��56   !   $��20��6   $��均線移動��6/   $
��Left=�6>   $��Center=�6Y   $
��Bold"�6t   $��30��6�   $��65��6�   $��Right=�6�   $��Center=�6�   $��14(�6�   $��5,0,0,0q�6�   ���	#FFFFCA006  �� 57   
   -   5        $ 均線移動數值��57   #   $��21��6   $
��Left=�63   $��30��6N   $
 Wrap=�6Z   $��0��6n   $��Center=�6w   $��65��6�   $��Center=�6�   $ False��6�   $|�False��6�   $��Center=�6�   $��14(�6  $��2,2,0,0q�6  $
��Bold"�6&  ���	#FF656565
�������
���   �6b  58      59      