   M S B A M L   `   `   `  ���� 	  Museo%    Museoapp.Views.ListaColecciones   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Icon$  ../ico/Colecciones.ico�� � WindowState$ 	Maximized=� � Title$ Colecciones��$
��1100��$	��800�� � WindowStartupLocation$ CenterScreen=����#7182B3.��� .+�o� $
��auto��$	��Top=�$
��auto�����#ecf0f3$��0,0.5,0,0.5q�
���   �.@�m� -    m�Name$ Libro��$��Libro��$��33��$
��Left=�.@�m� -   $ 
CrearLibro��$��Crear libro��m� -   $ ListaLi��$��Lista de libros��m� -   $ CrearColeccion��$��Crear coleccion��m� -   $ Archivo��$��Archivo��m� -   $ Autor��$��Autor��m� -   $ Material��$��Material���� 
�������$��10,102,10,0q�$	��Top=�._��  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��auto ��� $
��auto �.+��� $��0��$��Buscar��$��0��$��5q�$��50(��� -   $��0��$��1��
������ $��0q�$��0,15,0,0q�$��43��$��47��$��24(�.���� $��../img/Refresh.png��$��37���� $��1��$��0��$��
Por nombre��$��5q�$��24(��� -	    ��Name$ 	PorNombre��$��1��$��1��$�� ��$	��200��$��5q�$��24(��� -
   $��1��$��2��
������ $��0q�$��47��$��24(�.���� $��../img/Lupa.png��$��37���� $��1��$��3��$��Por lugar de procedencia��$��5q�$��24(��� -   $ Porlugar��$��1��$��4��$�� ��$	��200��$��5q�$��24(��� -   $��1��$��5��
������ $��0q�$��47��$��24(�.���� $��../img/Lupa.png��$��37���� $��1��$��6��$��Por Titulo/Alias��$��5q�$��24(��� -   $ 	PorTitulo��$��1��$��7��$�� ��$	��200��$��5q�$��24(��� -   $��1��$��8��
������ $��0q�$��47��$��24(�.���� $��../img/Lupa.png��$��37��&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$��10,330,10,10q�
����  �	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 Nombre��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Nombre+  $
 Cantidad�� �� *
Cantidad+  $
 Titulo Alias�� �� *Titulo_alias+  $
 Lugar Procedencia�� �� *Lugar_proce+  $
 Periodo�� �� *	Periodo+  $

 Alto�� �� *Alto+  $
 Ancho�� �� *Ancho+  $
 Largo�� �� *Largo+  $
 Diametro�� �� *
Diametro+  $
 
Integridad�� �� *
Integridad+  $
 Conservacion�� �� *Conservacion+  $
 	Ubicacion�� �� *	Ubicacion+  $
 Ingreso�� �� *	Ingreso+  $
 
Materiales�� �� *
Materiales+  $
 Autores�� �� *	Autores+4  .System.Windows.Controls.DataGridTemplateColumn  $

 Foto��   CellTemplate �� .R��� -   .���� $	��100��$	��100������ *Foto+     Width5  /System.Windows.Controls.DataGridLengthConverter$ 50  �� .R��� -   $��Eliminar��
���  ������    �� .R��� -   $��Editar��
���/������� 