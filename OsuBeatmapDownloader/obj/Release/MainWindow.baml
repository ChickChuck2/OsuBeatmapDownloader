   M S B A M L   `   `   `  ���� O  KOsuBeatmapDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:OsuBeatmapDownloaderOsuBeatmapDownloader  %    OsuBeatmapDownloader.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:OsuBeatmapDownloader     -     � Title$  Osu beatmap Downloader��$	��622��$	��870�����#050505 � 	Resources ��%    .��� ���#050505.+��� -    ��Name$ DownloadBtn��$��Download��$��
0,0,306,73q�$��Bottom=� J� RenderTransformOrigin$ 0.603,4.531&�$	��116��$��32��$��Right=��� -    ��Name$ CheckAdvancedFilter��$��Advanced Filters��$��
0,47,435,0q�$	��Top=�$��Right=�$	��156���� -    ��Name$ PathBox��$��
0,24,189,0q� �� TextWrapping$
 Wrap=�$��18��$	��Top=�$��Right=�$	��402���� -   $ SaveBtn��$
��Save��$��
0,22,103,0q�$	��Top=�$��Right=�$��81���� -   $ 	LocateBtn��$��Locate��$��	0,22,10,0q�$	��Top=�$��Right=�$��88���� $
��Auto=�$��	0,0,606,0q�.���� -    ��Name$ BeatmapList�� �� Orientation$ Vertical=�.+��� $ 
Horizontal=�.+��� $��
Difficulty��   BeamapDiffStyle#��[  � $��30���� $��Artist��  BeatmapArtistStyle#��[ � $��40���� $��Title��  BeatmapOsuFileStyle#��[ � $��40���� $��Creator��  BeatmapCreatorStyle#��[ �� -   	 ��Name$	 advancedFilterGrid��$��
0,79,189,0q�$	��337��$	��Top=�$��Right=�$	��402��.��� 
������ .+��� ���#121212
 �� CornerRadius$
 10���� $��Query��$��
9,10,0,309q�$
��Left=�$��39���� $��Modes��$��12,78,0,235q�$
��Left=�$��42���� -	    ��Name$ 
ComboModes��$��54,78,0,240q�$	��120��$��19��$
��Left=��� $��Status��$��9,132,0,184q�$
��Left=�$��44���� -
   $ ComboStatus��$��53,130,0,188q�$	��120��$��19��$
��Left=��� $��Genre��$��224,78,0,240q�$
��Left=�$��40���� -   $ 
ComboGenre��$��269,78,10,240q�$��19���� $��Language��$��206,127,0,187q�$
��Left=�$��58���� -   $ ComboLanguage��$��268,132,0,186q�$	��124��$��19��$
��Left=��� $��0,184,0,136q�$ 
Horizontal=�.+��� $��min_ar��� $��47���� -   $ minAr��$��50��� $	��135���� $��max_ar��� $��47���� -   $ maxAr��$��50���� $��0,206,0,114q�$ 
Horizontal=�.+��� $��min_od��� $��43���� -   $ minOd��$��50��� $	��135���� $��max_od��� $��43���� -   $ maxOd��$��50���� $��
0,228,0,92q�$ 
Horizontal=�.+��� $��minCs��� $��49���� -   $ minCs��$��50��� $	��135���� $��max_cs��� $��47���� -   $ maxCs��$��50���� $��
0,250,0,70q�$ 
Horizontal=�.+��� $��min_hp��� $��43���� -   $ minHp��$��50��� $	��135���� $��max_hp��� $��43���� -   $ maxHp��$��50���� $��
0,272,0,48q�$ 
Horizontal=�.+��� $��min_diff��� $��39���� -   $ minDiff��$��50��� $	��135���� $��max_diff��� $��40���� -   $ maxDiff��$��50���� $��
0,294,0,26q�$ 
Horizontal=�.+��� $��min_bpm��� $��32���� -   $ minBpm��$��50��� $	��135���� $��max_bpm��� $��32���� -   $ maxBpm��$��50���� $��	0,316,0,5q�$ 
Horizontal=�.+��� $��
min_lenght��� $��22���� -   $ 	minLenght��$��50��� $	��135���� $��
max_lenght��� $��23���� -   $ 	maxLenght��$��50���� -   $ QueryBox��$
��Left=�$��	48,10,0,0q�$
 Wrap=�$	��Top=�$	��344���� $��	0,8,492,0q�$
��Wrap=�$��Osu Songs Path��$	��Top=�$��Right=�$��99���� $��Offset��$��
0,47,435,0q�$��Right=�$��34��$��18��$	��Top=��� -   $ 	offsetBox��$��
0,47,380,0q�$
 Wrap=�$	��Top=�$��Right=�$��50���� -   $��?��$��
0,47,360,0q�$��Right=�$��16��$��18��$	��Top=��� -    ��Name$ countDownload��$��Downloaded:��$��	0,52,10,0q�$��Right=�$	��174��$��27��$	��Top=�