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
                            //caso eu esquecer [ = Array ; { = Object   ;   
                            Console.WriteLine(next);

                            JArray data = (JArray)json["data"];
                            Console.WriteLine(data);
                            JObject mapData = (JObject)data[next];

                            JArray childrenBeatmaps = (JArray)mapData["ChildrenBeatmaps"];

                            //Data
                            GetBeatmap getBeatmap = new GetBeatmap();

                            JToken SetId = mapData["SetId"];
                            getBeatmap.SetID = (int)SetId;
                            JToken RankedStatus = mapData["RankedStatus"];
                            getBeatmap.RankedStatus = (int)RankedStatus;
                            JToken ApprovedDate = mapData["ApprovedDate"];
                            getBeatmap.ApprovedDate = (string)ApprovedDate;
                            JToken LastUpdate = mapData["LastUpdate"];
                            getBeatmap.LastUpdate = (string)LastUpdate;
                            JToken LastChecked = mapData["LastChecked"];
                            getBeatmap.LastChecked = (string)LastChecked;
                            JToken Artist = mapData["Artist"];
                            getBeatmap.Artist = (string)Artist;
                            JToken Title = mapData["Title"];
                            getBeatmap.Title = (string)Title;
                            JToken Creator = mapData["Creator"];
                            getBeatmap.Creator = (string)Creator;
                            JToken Source = mapData["Source"];
                            getBeatmap.Source = (string)Source;
                            JToken Tags = mapData["Tags"];
                            getBeatmap.Tags = (string)Tags;
                            JToken HasVideo = mapData["HasVideo"];
                            getBeatmap.HasVideo = (bool)HasVideo;
                            JToken Genre = mapData["Genre"];
                            getBeatmap.Genre = (int)Genre;
                            JToken Language = mapData["Language"];
                            getBeatmap.Language = (int)Language;
                            JToken Favourites = mapData["Favourites"];
                            getBeatmap.Favourites = (int)Favourites;
                            JToken Disabled = mapData["Disabled"];
                            getBeatmap.Disabled = (int)Disabled;

                            //Children
                            bool containsChildren = childrenBeatmaps.Count > 0;

                            JToken BeatmapId = (containsChildren) ? childrenBeatmaps[0]["BeatmapId"] : null;
                            JToken ParentSetId = (containsChildren) ? childrenBeatmaps[0]["ParentSetId"] : null;
                            JToken DiffName = (containsChildren) ? childrenBeatmaps[0]["DiffName"] : null;
                            JToken FileMD5 = (containsChildren) ? childrenBeatmaps[0]["FileMD5"] : null;
                            JToken Mode = (containsChildren) ? childrenBeatmaps[0]["Mode"] : null;
                            JToken BPM = (containsChildren) ? childrenBeatmaps[0]["BPM"] : null;
                            JToken AR = (containsChildren) ? childrenBeatmaps[0]["AR"] : null;
                            JToken OD = (containsChildren) ? childrenBeatmaps[0]["OD"] : null;
                            JToken CS = (containsChildren) ? childrenBeatmaps[0]["CS"] : null;
                            JToken HP = (containsChildren) ? childrenBeatmaps[0]["HP"] : null;
                            JToken TotalLength = (containsChildren) ? childrenBeatmaps[0]["TotalLength"] : null;
                            JToken HitLength = (containsChildren) ? childrenBeatmaps[0]["HitLength"] : null;
                            JToken Playcount = (containsChildren) ? childrenBeatmaps[0]["Playcount"] : null;
                            JToken Passcount = (containsChildren) ? childrenBeatmaps[0]["Passcount"] : null;
                            JToken MaxCombo = (containsChildren) ? childrenBeatmaps[0]["MaxCombo"] : null;
                            JToken DifficultyRating = (containsChildren) ? childrenBeatmaps[0]["DifficultyRating"] : null;
                            JToken OsuFile = (containsChildren) ? childrenBeatmaps[0]["OsuFile"] : null;
                            JToken DownloadPath = (containsChildren) ? childrenBeatmaps[0]["DownloadPath"] : null;

                            if (childrenBeatmaps.Count > 0)
                            {
                                getBeatmap.BeatmapId = (int)BeatmapId;
                                getBeatmap.ParentSetId = (int)ParentSetId;
                                getBeatmap.DiffName = (string)DiffName;
                                getBeatmap.FileMD5 = (string)FileMD5;
                                getBeatmap.Mode = (int)Mode;
                                getBeatmap.BPM = (int)BPM;
                                getBeatmap.AR = (float)AR;
                                getBeatmap.OD = (float)OD;
                                getBeatmap.CS = (float)CS;
                                getBeatmap.HP = (float)HP;
                                getBeatmap.TotalLength = (int)TotalLength;
                                getBeatmap.HitLength = (int)HitLength;
                                getBeatmap.Playcount = (int)Playcount;
                                getBeatmap.Passcount = (int)Passcount;
                                getBeatmap.MaxCombo = (int)MaxCombo;
                                getBeatmap.DifficultyRating = (double)DifficultyRating;
                                getBeatmap.OsuFile = (string)OsuFile;
                                getBeatmap.DownloadPath = (string)DownloadPath;
                            }

                            bool ContainsChildInList = containsChildren && ConfigManager.GetBeatmaps().Contains((int)childrenBeatmaps[0]["ParentSetId"]);
                            bool containsBeamap = ConfigManager.GetBeatmaps().Contains((int)SetId);
                            Console.WriteLine(Url);
                            if (containsBeamap || containsChildren && !ContainsChildInList)
                            {
                                JToken wdownload = (!containsBeamap) ? SetId : ParentSetId;
                                string DownloadURL = $"https://api.chimu.moe/v1/d/{wdownload}";
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

                                            if(!Directory.Exists(main.PathBox.Text))
                                                Directory.CreateDirectory(main.PathBox.Text);

                                            if (!File.Exists($@"{main.PathBox.Text}\{fileName}"))
                                                if (fileinfo.Length > 0)
                                                    try
                                                    {
                                                        File.Move($@"{TEMP}\{fileName}", $@"{main.PathBox.Text}\{fileName}");
                                                        BeatmapInfo.CreateBeatmapInformation(main, getBeatmap);
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
                })
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
