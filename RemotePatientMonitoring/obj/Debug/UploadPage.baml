   M S B A M L   `   `   `  ����R  NRemotePatientMonitoring, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RecipeShareRecipeShare  5   	   "    RecipeShare.RecipeUploadPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RecipeShare     5   	     � Title$  Upload Page��5   	   $	��450��6   $	��800��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��304* �6   �� 5      $
��131* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF4000FF��6   � 5      $ 	#FF00D4FF��6    � Offset$ 0.898Q�61   5      .+�5   
   �� -   5         ��Name$ RecipeNameTextBox��5      $
��Left=�6-   $��54��6H   $��	64,92,0,0q�6T    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��143��6�   �� 5   
   -   5         ��Name$ 	btnUpload��5      $��Upload��6$   $
��Left=�65   $��53��6P   $��
64,164,0,0q�6\   $	��Top=�6p   $	��112��6�   6�   �� 5   
   -   5        $ btnSave��5      $
��Save��6"   $
��Left=�61   $��53��6L   $��351,164,0,0q�6X   $	��Top=�6m   $	��112��6�   6�   �� 5   
   -   5        	 ��Name$	 RecipesComboBox��5      $
��Left=�6,   $	��108��6G   $��
543,65,0,0q�6T   $	��Top=�6h   $	��231��6�   6�   �� 5"   
   -   5        $ FileNameTextBox��5"      $
��Left=�6+   $��53��6F   $��
212,92,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $	��251��6�   �� 5#   
   -   5        $ 	btnRemove��5#      $��Remove��6$   $
��Left=�65   $��44��6P   $��541,212,0,0q�6\   $	��Top=�6q   $��92��6�   6�   �� 5$   
   -   5        $ 
ViewRecipe��5$      $
��View��6%   $
��Left=�64   $��44��6O   $��685,212,0,0q�6[   $	��Top=�6p   $��89��6�   6�   �� 5%   
   $��Name Your Recipe��6   $
��Left=�6+   $��37��6F   $��	45,35,0,0q�6R   $	��Top=�6e   $	��181��6}   ���	#FFF8F3F3$��20(�6�   $��Amiri)�6�   $
��Bold"�6�   �� 5&   
   $��Recipe Path��6   $
��Left=�6&   $��34��6A   $��
264,35,0,0q�6M   $	��Top=�6a   $	��148��6y   ���	#FFF6EDED$��Amiri)�6�   $��20(�6�   $
��Bold"�6�   �� 5'   
   $��Recipe Corner��6   $
��Left=�6(   $��50��6C   $��
587,10,0,0q�6O   $	��Top=�6c   $	��143��6{   $��Amiri)�6�   $��20(�6�   $
��Bold"�6�   ���	#FFFCF8F86�   �� 5(   
   -   5        $ btnHome��5(      $��	Main Page��69   $
��Left=�6N   $��48��6i   $��	31,45,0,0q�6u   $	��Top=�6�   $	��105��6�   $��1��6�   5)      5*      