using System;
using System.Diagnostics;
using System.Windows;

namespace OsuBeatmapDownloader.Scripts
{
    /// <summary>
    /// Lógica interna para BeatmapInformations.xaml
    /// </summary>
    public partial class BeatmapInformations : Window
    {
        public BeatmapInformations()
        {
            InitializeComponent();
        }

        public void PutValues(GetBeatmap getBeatmap)
        {
            BeatmapName.Text = getBeatmap.Title;

            Artist.Text +=      $"[{getBeatmap.Artist}]";
            Title.Text +=       $"[{getBeatmap.Title}]";
            Creator.Text +=     $"[{getBeatmap.Creator}]";
            DiffName.Text +=    $"[{getBeatmap.DiffName}]";
            RankedStatus.Text +=$"[{getBeatmap.RankedStatus}]";
            ApprovedDate.Text +=$"[{getBeatmap.ApprovedDate}]";
            Genre.Text +=       $"[{getBeatmap.Genre}]";
            Language.Text +=    $"[{getBeatmap.Language}]";
            PlayCount.Text +=   $"[{getBeatmap.Playcount}]";
            PassCount.Text +=   $"[{getBeatmap.Passcount}]";
            MaxCombo.Text +=    $"[{getBeatmap.MaxCombo}]";
            Mode.Text +=        $"[{getBeatmap.Mode}]";
            BPM.Text +=         $"[{getBeatmap.BPM}]";
            AR.Text +=          $"[{getBeatmap.AR}]";
            OD.Text +=          $"[{getBeatmap.OD}]";
            CS.Text +=          $"[{getBeatmap.CS}]";
            HP.Text +=          $"[{getBeatmap.HP}]";
            TotalLength.Text += $"[{getBeatmap.TotalLength}]";
            HitLength.Text +=   $"[{getBeatmap.HitLength}]";
            SetId.Text +=       $"[{getBeatmap.SetID}]";
            LastUpdate.Text +=  $"[{getBeatmap.LastUpdate}]";
            LastChecked.Text += $"[{getBeatmap.LastChecked}]";
            Source.Text +=      $"[{getBeatmap.Source}]";
            HasVideo.Text +=    $"[{getBeatmap.HasVideo}]";
            Favourites.Text +=  $"[{getBeatmap.Favourites}]";
            Disabled.Text +=    $"[{getBeatmap.Disabled}]";
            BeatmapID.Text +=   $"[{getBeatmap.BeatmapId}]";
            ParentSetId.Text += $"[{getBeatmap.ParentSetId}]";
            FileMD5.Text +=     $"[{getBeatmap.FileMD5}]";
            OsuFile.Text +=     $"[{getBeatmap.OsuFile}]";
            DownloadPath.Text +=$"[{getBeatmap.DownloadPath}]";

            link.Text = $@"https://api.chimu.moe/v1{getBeatmap.DownloadPath}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void link_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(link.Text);
        }
    }
}
