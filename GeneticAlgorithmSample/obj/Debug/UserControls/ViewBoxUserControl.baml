   M S B A M L   `   `   `  ����Q  MGeneticAlgorithmSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:GeneticAlgorithmSampleGeneticAlgorithmSample  5      5    /GeneticAlgorithmSample.Views.ViewBoxUserControl   5   
   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:GeneticAlgorithmSample     5        (� Stretch$
  Fill=�5   
   .��5      �� -   5         ��Name$	 lst��5   &   $	��500��5      $	��500��6   
������ ��5      �� ��5   
   �� 5      .R�5      ��  K� IsItemsHost . 6   5      ��5   
   �� 5       �� 
TargetType" ��6   .�5      ��  �� Property �@�FocusVisualStyle �� Value 5      �� 5      " ��6    .�5      ��  �@J�Opacity$ 0Q�68   5      5      �� 5       �@��
IsSelected 6/   �� *
IsSelected+�� 5"       � �� 5#      �� 5$       �� 
TargetType" ��6*   .R�5&      ��  ��Name$
 Line��5,   +   -   5        
���  ��	 �� X1 5&   #   �� *X1+		 �� Y1	 65   �� *Y1+	
 �� X2
 5'   +   �� *X2+	 �� Y2 6=   �� *Y2+��5(   +   �� *	Thickness+ J� Opacity 5)   +   �� *	Opacity+5/       �� Triggers 50      P� 51   "    P� Property �@��
IsSelected P� Value . . �52   &   ��  �� 
TargetName$
 Line��6?    �@J�Effect 53   *   3  -System.Windows.Media.Effects.DropShadowEffect  54   .      Color$ CornflowerBlue��6?      ShadowDepth$ 3Q�6V      
BlurRadius$ 10Q�6f   56   '   57   #   59      5;      5<      5>      5?      