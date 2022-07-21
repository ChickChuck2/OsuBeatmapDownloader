using Newtonsoft.Json.Linq;
using OsuBeatmapDownloader.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OsuBeatmapDownloader
{
    /// <summary>
    /// Interação lógica para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static class PlayMode
        {
            public enum PlayModes
            {
                Osu,
                Taiko,
                CatchTheBeat,
                Mania
            }
        }
        public static class RankedStatus
        {
            public enum Rankedstatus
            {
                Graveyard = -2,
                WIP,
                Pending,
                Ranked,
                Approved,
                Qualified,
                Loved
            }
        }
        public static class Genres
        {
            public enum genre
            {
                Any,
                Unspecified,
                Game,
                Anime,
                Rock,
                Pop,
                Other,
                Novelty,
                HipHop= 9,
                Eletrnoic,
                Metal,
                Classic,
                PopularMusic,
                Jazz

            }
        }
        public static class Languages
        {
            public enum language
            {
                Any,
                NotSpecific,
                English,
                Japanese,
                Chinese,
                Instrumental,
                Korean,
                French,
                German,
                Swedish,
                Spanish,
                Italian,
                Russian,
                Polish,
                Other,

            }
        }
        public static class SearchOpitions
        {
            /// <summary>
            /// Pesquisa em geral
            /// </summary>
            public static string query { get; set; }

            /// <summary>
            /// Quantidade de Beatmap que aparece na API
            /// </summary>
            public static int amount { get; set; }

            /// <summary>
            /// sei la porra
            /// </summary>
            public static int offset { get; set; }

            /// <summary>
            /// RankedStatus
            /// </summary>
            public static int status { get; set; }

            /// <summary>
            /// Play Mode
            /// </summary>
            public static int mode { get; set; }

            public static float min_ar { get; set; }
            public static float max_ar { get; set; }
            public static float min_od { get; set; }
            public static float max_od { get; set; }
            public static float min_cs { get; set; }
            public static float max_cs { get; set; }
            public static float min_hp { get; set; }
            public static float max_hp { get; set; }
            public static float min_diff { get; set; }
            public static float max_diff { get; set; }
            public static float min_bpm { get; set; }
            public static float max_bpm { get; set; }
            public static float min_lenght { get; set; }
            public static float max_lenght { get; set; }
        };

        public string OsuPath;

        ChimuDownloader chimu = new ChimuDownloader();

        private NewComboboxItem GetBoxContent(ComboBox box)
        {
            return box.SelectedItem as NewComboboxItem;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ConfigManager.FirstInitialize();
            if (ConfigManager.HavePath())
                PathBox.Text = ConfigManager.GetPath();

            countDownload.Text = $"Downloaded: {ConfigManager.GetBeatmaps().Count}";
            advancedFilterGrid.IsEnabled = CheckAdvancedFilter.IsChecked.Value;
            ForeachKeyValue(typeof(RankedStatus.Rankedstatus), ComboStatus);
            ForeachKeyValue(typeof(PlayMode.PlayModes), ComboModes);
            ForeachKeyValue(typeof(Genres.genre), ComboGenre);
            ForeachKeyValue(typeof(Languages.language), ComboLanguage);
        }

        private List<KeyValuePair<string, int>> ForeachKeyValue(Type Enumer, ComboBox box)
        {
            string[] Key = Enum.GetNames(Enumer);
            int[] Value = (int[])Enum.GetValues(Enumer);
            List<KeyValuePair<string, int>> iri = new List<KeyValuePair<string, int>>() {};

            List<string> keyer = new List<string>();
            List<int> valuer = new List<int>();

            foreach (string key in Key)
                keyer.Add(key);
            foreach (int value in Value)
                valuer.Add(value);
            for (int i = 0; i < valuer.Count; i++)
                iri.Add(new KeyValuePair<string, int>(keyer[i], valuer[i]));
            foreach (KeyValuePair<string, int> ir in iri)
                box.Items.Add(new NewComboboxItem { Text = ir.Key, Value = ir.Value });
            return iri;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(PathBox.Text))
                ConfigManager.Save(PathBox.Text);
            else
                MessageBox.Show(this, "Directory Don't Exist");
        }

        private void CheckAdvancedFilter_Changed(object sender, RoutedEventArgs e) => advancedFilterGrid.IsEnabled = CheckAdvancedFilter.IsChecked.Value;
        private void DownloadBtn_Click(object sender, RoutedEventArgs e) => chimu.Start(this);
        public static HttpStatusCode GetRequestCode(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpStatusCode status = response.StatusCode;
                if(request.Timeout > 5000)
                    return HttpStatusCode.GatewayTimeout;
                return status;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HttpStatusCode.GatewayTimeout;
            }
        }

        private void LocateBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();

            string Path = "";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Path = dialog.SelectedPath;

            PathBox.Text = Path;
            OsuPath = Path;

        }
        public string SwitchCharacters(string FileName)
        {
            string illegal = FileName;
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            illegal = r.Replace(illegal, "");

            return illegal;
        }

        public void TextBoxPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        public JObject GetJson(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return JObject.Parse(reader.ReadToEnd());
        }

        public string GetFilters()
        {
            string Filters = "";

            Dispatcher.Invoke(() =>
            {
                if (CheckAdvancedFilter.IsChecked.Value)
                {

                    if (QueryBox.Text != "")
                        Filters += $"&query={QueryBox.Text}";
                    if (ComboStatus.SelectedIndex != -1)
                        Filters += $"&status={(int)GetBoxContent(ComboStatus).Value}";
                    if (ComboModes.SelectedIndex != -1)
                        Filters += $"&mode={(int)GetBoxContent(ComboModes).Value}";
                    if (ComboGenre.SelectedIndex != -1)
                        Filters += $"&genre={(int)GetBoxContent(ComboGenre).Value}";
                    if (ComboLanguage.SelectedIndex != -1)
                        Filters += $"&language={(int)GetBoxContent(ComboLanguage).Value}";
                    if (minOd.Text != "")
                        Filters += $"&min_od={int.Parse(minOd.Text)}";
                    if (maxOd.Text != "")
                        Filters += $"&max_od={int.Parse(maxOd.Text)}";
                    if (minCs.Text != "")
                        Filters += $"&min_cs={int.Parse(minCs.Text)}";
                    if (maxCs.Text != "")
                        Filters += $"&max_cs={int.Parse(maxCs.Text)}";
                    if (minAr.Text != "")
                        Filters += $"&min_ar={int.Parse(minAr.Text)}";
                    if (maxAr.Text != "")
                        Filters += $"&max_ar={int.Parse(maxAr.Text)}";
                    if (minHp.Text != "")
                        Filters += $"&min_hp={int.Parse(minHp.Text)}";
                    if (maxHp.Text != "")
                        Filters += $"&max_hp={int.Parse(maxHp.Text)}";
                    if (minDiff.Text != "")
                        Filters += $"&min_diff={int.Parse(minDiff.Text)}";
                    if (maxDiff.Text != "")
                        Filters += $"&max_diff={int.Parse(maxDiff.Text)}";
                    if (minBpm.Text != "")
                        Filters += $"&min_bpm={int.Parse(minBpm.Text)}";
                    if (maxBpm.Text != "")
                        Filters += $"&max_bpm={int.Parse(maxBpm.Text)}";
                    if (minLenght.Text != "")
                        Filters += $"&min_length={int.Parse(minLenght.Text)}";
                    if (maxLenght.Text != "")
                        Filters += $"&max_length={int.Parse(maxLenght.Text)}";
                }
            });
            return Filters;
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Modify the offset value if the beatmaps are taking a long time to start downloading (This happens when you have already downloaded too many maps. offset is equal to the amount of beatmaps you have already downloaded)");
    }
}