using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace OsuBeatmapDownloader.Scripts
{
    class ChimuDownloader
    {
        bool Downloading = false;
        bool BreakDownload = false;
        public void Start(MainWindow main)
        {
            async void StartDownload(int offset = 0)
            {
                Downloading = true;
                bool canDownloadOther = true;
                int next = 0;
                if (main.offsetBox.Text.Length != 0)
                    offset = int.Parse(main.offsetBox.Text);

                string Url = $"https://api.chimu.moe/v1/search?offset={offset}" + main.GetFilters();
                JObject json = main.GetJson(Url);

                await Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        if (BreakDownload == true)
                        {
                            Downloading = false;
                            BreakDownload = false;
                            break;
                        }
                        if (canDownloadOther)
                        {
                            JToken confi = json["data"][next];
                            JToken childrenBeatmaps = confi["ChildrenBeatmaps"][0];
                            //Data
                            GetBeatmap getBeatmap = new GetBeatmap();

                            JToken SetId = confi["SetId"];
                            getBeatmap.SetID = (int)SetId;
                            JToken RankedStatus = confi["RankedStatus"];
                            getBeatmap.RankedStatus = (int)RankedStatus;
                            JToken ApprovedDate = confi["ApprovedDate"];
                            getBeatmap.ApprovedDate = (string)ApprovedDate;
                            JToken LastUpdate = confi["LastUpdate"];
                            getBeatmap.LastUpdate = (string)LastUpdate;
                            JToken LastChecked = confi["LastChecked"];
                            getBeatmap.LastChecked = (string)LastChecked;
                            JToken Artist = confi["Artist"];
                            getBeatmap.Artist = (string)Artist;
                            JToken Title = confi["Title"];
                            getBeatmap.Title = (string)Title;
                            JToken Creator = confi["Creator"];
                            getBeatmap.Creator = (string)Creator;
                            JToken Source = confi["Source"];
                            getBeatmap.Source = (string)Source;
                            JToken Tags = confi["Tags"];
                            getBeatmap.Tags = (string)Tags;
                            JToken HasVideo = confi["HasVideo"];
                            getBeatmap.HasVideo = (bool)HasVideo;
                            JToken Genre = confi["Genre"];
                            getBeatmap.Genre = (int)Genre;
                            JToken Language = confi["Language"];
                            getBeatmap.Language = (int)Language;
                            JToken Favourites = confi["Favourites"];
                            getBeatmap.Favourites = (int)Favourites;
                            JToken Disabled = confi["Disabled"];
                            getBeatmap.Disabled = (int)Disabled;

                            //Children
                            JToken BeatmapId = childrenBeatmaps["BeatmapId"];
                            getBeatmap.BeatmapId = (int)BeatmapId;
                            JToken ParentSetId = childrenBeatmaps["ParentSetId"];
                            getBeatmap.ParentSetId = (int)ParentSetId;
                            JToken DiffName = childrenBeatmaps["DiffName"];
                            getBeatmap.DiffName = (string)DiffName;
                            JToken FileMD5 = childrenBeatmaps["FileMD5"];
                            getBeatmap.FileMD5 = (string)FileMD5;
                            JToken Mode = childrenBeatmaps["Mode"];
                            getBeatmap.Mode = (int)Mode;
                            JToken BPM = childrenBeatmaps["BPM"];
                            getBeatmap.BPM = (int)BPM;
                            JToken AR = childrenBeatmaps["AR"];
                            getBeatmap.AR = (float)AR;
                            JToken OD = childrenBeatmaps["OD"];
                            getBeatmap.OD = (float)OD;
                            JToken CS = childrenBeatmaps["CS"];
                            getBeatmap.CS = (float)CS;
                            JToken HP = childrenBeatmaps["HP"];
                            getBeatmap.HP = (float)HP;
                            JToken TotalLength = childrenBeatmaps["TotalLength"];
                            getBeatmap.TotalLength = (int)TotalLength;
                            JToken HitLength = childrenBeatmaps["HitLength"];
                            getBeatmap.HitLength = (int)HitLength;
                            JToken Playcount = childrenBeatmaps["Playcount"];
                            getBeatmap.Playcount = (int)Playcount;
                            JToken Passcount = childrenBeatmaps["Passcount"];
                            getBeatmap.Passcount = (int)Passcount;
                            JToken MaxCombo = childrenBeatmaps["MaxCombo"];
                            getBeatmap.MaxCombo = (int)MaxCombo;
                            JToken DifficultyRating = childrenBeatmaps["DifficultyRating"];
                            getBeatmap.DifficultyRating = (double)DifficultyRating;
                            JToken OsuFile = childrenBeatmaps["OsuFile"];
                            getBeatmap.OsuFile = (string)OsuFile;
                            JToken DownloadPath = childrenBeatmaps["DownloadPath"];
                            getBeatmap.DownloadPath = (string)DownloadPath;

                            bool ContainsInList = ConfigManager.GetBeatmaps().Contains((int)childrenBeatmaps["ParentSetId"]);
                            Console.WriteLine(Url);
                            if (!ContainsInList)
                            {
                                string DownloadURL = $"https://api.chimu.moe/v1/d/{ParentSetId}";
                                HttpStatusCode code = MainWindow.GetRequestCode($"https://api.chimu.moe/{DownloadPath}");
                                main.Dispatcher.Invoke(() =>
                                {
                                    Console.WriteLine($"https://api.chimu.moe/{DownloadPath} >> {code}");
                                    if (code != HttpStatusCode.GatewayTimeout)
                                    {
                                        string TEMP = Environment.GetEnvironmentVariable("TEMP");
                                        string fileName = $"{main.SwitchCharacters((string)OsuFile)}.osz";
                                        WebClient client = new WebClient();

                                        client.DownloadFileAsync(new Uri(DownloadURL), $@"{TEMP}\{fileName}");

                                        client.DownloadFileCompleted += new AsyncCompletedEventHandler((sender1, e1) =>
                                        {
                                            Console.WriteLine("Moving File to Osu Folder");

                                            FileInfo fileinfo = new FileInfo($@"{TEMP}\{fileName}");

                                            if (!File.Exists($@"{main.PathBox.Text}\{fileName}"))
                                                if (fileinfo.Length > 0)
                                                    try
                                                    {
                                                        File.Move($@"{TEMP}\{fileName}", $@"{main.PathBox.Text}\{fileName}");
                                                        BeatmapInfo.CreateBeatmapInformation(main,getBeatmap);
                                                        ConfigManager.SaveParentSetId((int)ParentSetId);
                                                    }
                                                    catch { }
                                                else { ConfigManager.SaveParentSetId((int)ParentSetId); }
                                            else
                                                try
                                                {
                                                    File.Delete($@"{TEMP}\{fileName}");
                                                }
                                                catch { }
                                        });
                                        main.countDownload.Text = $"Downloaded: {ConfigManager.GetBeatmaps().Count}";
                                    }
                                    else
                                    {
                                        ConfigManager.SaveParentSetId((int)ParentSetId);
                                        BeatmapInfo.CreateBeatmapInformation(main, getBeatmap, true);
                                    };
                                });
                            }
                            else Console.WriteLine($"You have the beatmap {next}");
                            next++;
                            if (next >= 49)
                            {
                                next = 0;
                                offset += 50;
                                main.Dispatcher.Invoke(() => StartDownload(offset));
                                break;
                            }
                        }
                    }
                });
            }
            if (Downloading == false)
            {
                StartDownload();
                main.DownloadBtn.Content = "Stop";
            }
            else
            {
                BreakDownload = true;
                main.DownloadBtn.Content = "Start";
            }
        }
    }
}
