using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OsuBeatmapDownloader.Scripts
{
    internal class BeatmapInfo
    {
        public static void CreateBeatmapInformation(MainWindow main,GetBeatmap getBeatmap, bool error = false)
        {
            StackPanel informations = new StackPanel() { Orientation = Orientation.Horizontal };
            BeatmapInformations informations1 = new BeatmapInformations();
            informations.MouseDown += new System.Windows.Input.MouseButtonEventHandler((sender, e) =>
            {
                informations1.Show();
                informations1.PutValues(getBeatmap);
            });

            TextBlock DiffBlock = new TextBlock()
            {
                Text = $"[{getBeatmap.DiffName}] ",
                Style = main.FindResource("BeamapDiffStyle") as Style
            };
            if (error == true)
                DiffBlock.Text = $"[ERROR 504 GatewayTimeout]";

            TextBlock ArtistBlock = new TextBlock()
            {
                Text = $"{getBeatmap.Artist} - ",
                Style = main.FindResource("BeatmapArtistStyle") as Style
            };

            TextBlock TitleBlock = new TextBlock()
            {
                Text = $"{getBeatmap.Title} ",
                Style = main.FindResource("BeatmapOsuFileStyle") as Style
            };

            TextBlock CreatorBlock = new TextBlock()
            {
                Text = $"{getBeatmap.Creator} ",
                Style = main.FindResource("BeatmapCreatorStyle") as Style
            };

            informations.Children.Add(DiffBlock);
            informations.Children.Add(new Grid() { Width = 20 });
            informations.Children.Add(ArtistBlock);
            informations.Children.Add(new Grid() { Width = 20 });
            informations.Children.Add(TitleBlock);
            informations.Children.Add(new Grid() { Width = 20 });
            informations.Children.Add(CreatorBlock);
            main.BeatmapList.Children.Add(informations);
        }
    }
}
