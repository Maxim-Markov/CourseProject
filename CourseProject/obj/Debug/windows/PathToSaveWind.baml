   M S B A M L   `   `   `  ����H  DCourseProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CourseProjectCourseProject  5      "    CourseProject.PathToSaveWind   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -localclr-namespace:CourseProject     5        � Title$;  5Сохранение результата в файл��5       � WindowStartupLocation$ CenterScreen=�6&    � 
ResizeMode$ NoResize=�6K    � SizeToContent$ WidthAndHeight=�6a   $	��288��6�    � Icon$ ../images/cat.ico��6�   $��174.855��6�   .��5	      � $��
10,10,10,2q�6    � RowDefinitions 5
   
   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��36 �6   �� 5      $
��Auto �6   .+�5   
   �� A?Введите путь для сохранения файла:68   �� 5   
   -   5        $��PathBox��5      $��1��6!   $	��250��6.   $��0,0,0,16q�6=   $��2��6O   .��6`   С:\6e   �� 5   
   $��2��6   $��	0,10,0,12q�6!   $��2��64   #!Введите имя файла:6Y   �� 5   
   -   5        $��FileNameBox��5      $��3��6%   $	��250��62   $��0,10,0,0q�6A   � 5   
   $��4��6   $��Right=�6!   $��
0,10,10,22q�6=   $	��130��6Q   .+�5      �� -   5         �� 	IsDefault . $��60��5   ;   $��0,0,10,0q�6I   $��OK��6[   �� 5       �� IsCancel . $��60��6%   $��Отмена��63   5      5      5      